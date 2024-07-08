using NLog.LayoutRenderers;
using TrueSync;
using Unity.Mathematics;

namespace ET.Client
{
    //
    [Invoke(TimerInvokeType.BattleRound)]
    public class AdventureBattleRoundTimer : ATimer<AdventureComponent>
    {
        protected override void Run(AdventureComponent t)
        {
            t?.PlayOneBattleRound().Coroutine();
        }
    }

    [FriendOfAttribute(typeof(ET.Client.AdventureComponent))]
    public static class AdventureComponentSystem
    {

        public static void SetBattleRandomSeed(this AdventureComponent self)
        {
            int seed = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene()).GetComponent<NumericComponent>()
                    .GetAsInt(NumericType.BattleRandomSeed);
            if (self.Random == null)
            {
                self.Random = TSRandom.New(seed);
            }
            else
            {
                self.Random.Initialize(seed);
            }
        }
        
        //重置游戏数据
        public static void ResetAdventure(this AdventureComponent self)
        {
            for (int i = 0; i < self.EnemyIdList.Count; i++)
            {
                self.Root().CurrentScene().GetComponent<UnitComponent>().Remove(self.EnemyIdList[i]);
            }

            self.Root().GetComponent<TimerComponent>().Remove(ref self.BattleTimer);
            self.BattleTimer = 0;
            self.Round = 0;
            self.EnemyIdList.Clear();
            self.AliveEnemyIdList.Clear();

            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene());
            int maxHp = unit.GetComponent<NumericComponent>().GetAsInt(NumericType.MaxHp);
            unit.GetComponent<NumericComponent>().Set(NumericType.Hp, maxHp);
            unit.GetComponent<NumericComponent>().Set(NumericType.IsAlive, 1);
            
            EventSystem.Instance.Publish(self.Root().CurrentScene(),new AdventureRoundReset());
        }

        //开始冒险
        public static async ETTask StartAdventure(this AdventureComponent self)
        {
            self.ResetAdventure();
            await self.CreateAdventureEnemy();
            self.ShowAdventureHpBarInfo(true);
            self.BattleTimer = self.Root().GetComponent<TimerComponent>().NewOnceTimer(TimeInfo.Instance.ServerNow() + 500, TimerInvokeType.BattleRound, self);
        }

        public static void ShowAdventureHpBarInfo(this AdventureComponent self, bool isShow)
        {
            Unit myUnit = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene());
            EventSystem.Instance.Publish(self.Root().CurrentScene(), new ShowAdventureHpBar() {Unit = myUnit, isShow = isShow});
            for (int i = 0; i < self.EnemyIdList.Count; i++)
            {
                Unit monsterUnit = self.Root().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                EventSystem.Instance.Publish(self.Root().CurrentScene(), new ShowAdventureHpBar(){Unit = monsterUnit, isShow = isShow});
            }
        }

        //根据关卡id创建出怪物
        public static async ETTask CreateAdventureEnemy(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene());
            int levelId = unit.GetComponent<NumericComponent>().GetAsInt(NumericType.AdventureState);

            BattleLevelConfig battleLevelConfig = BattleLevelConfigCategory.Instance.Get(levelId);
            for (int i = 0; i < battleLevelConfig.MonsterIds.Length; i++)
            {
                Unit monsterUnit = await UnitFactory.CreateMonster(self.Root().CurrentScene(), battleLevelConfig.MonsterIds[i]);
                //Position中会抛事件
                monsterUnit.Position = new float3(1.5f, -2 + i, 0);
                self.EnemyIdList.Add(monsterUnit.Id);
            }
        }

        public static async ETTask PlayOneBattleRound(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene());
            if (self.Round % 2 == 0)
            {
                //玩家回合
                Unit monsterUnit = self.GetTargetMonsterUnit();
                EventSystem.Instance.PublishAsync(self.Root().CurrentScene(), new AdventureBattleRoundView(){ TargeUnit = monsterUnit, AttackUnit = unit}).Coroutine();
                await EventSystem.Instance.PublishAsync(self.Root().CurrentScene(),new AdventureBattleRound(){ TargeUnit = monsterUnit, AttackUnit = unit});

                await self.Root().GetComponent<TimerComponent>().WaitAsync(1000);
            }
            else
            {
                //敌人回合
                for (int i = 0; i < self.EnemyIdList.Count; i++)
                {
                    if (!unit.isAlive())
                    {
                        break;
                    }

                    Unit monsterUnit = self.Root().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);

                    if (!monsterUnit.isAlive())
                    {
                        continue;
                    }
                    EventSystem.Instance.PublishAsync(self.Root().CurrentScene(), new AdventureBattleRoundView(){ TargeUnit = unit, AttackUnit = monsterUnit}).Coroutine();
                    await EventSystem.Instance.PublishAsync(self.Root().CurrentScene(), new AdventureBattleRound(){ TargeUnit = unit, AttackUnit = monsterUnit});
                    await self.Root().GetComponent<TimerComponent>().WaitAsync(1000);
                }
            }

            self.BattleRoundOver();
        }

        public static void BattleRoundOver(this AdventureComponent self)
        {
            ++self.Round;
            BattleRoundResult battleRoundResult = self.GetBattleRoundResult();
            Log.Debug("当前回合结果 ：" + battleRoundResult);
            switch (battleRoundResult)
            {
                case BattleRoundResult.KeepBattle:
                    self.BattleTimer = self.Root().GetComponent<TimerComponent>().NewOnceTimer(TimeInfo.Instance.ServerNow() + 500, TimerInvokeType.BattleRound, self);
                    break;
                case BattleRoundResult.WinBattle:
                    Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene());
                    EventSystem.Instance.PublishAsync(self.Root().CurrentScene(),new AdventureBattleOver() { WinUnit = unit }).Coroutine();
                    break;
                case BattleRoundResult.LoseBattle:
                    for (int i = 0; i < self.EnemyIdList.Count; i++)
                    {
                        Unit monsterUnit = self.Root().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                        if (!monsterUnit.isAlive())
                        {
                            continue;
                        }
                        EventSystem.Instance.PublishAsync(self.Root().CurrentScene(),new AdventureBattleOver() {WinUnit = monsterUnit }).Coroutine();
                    }
                    break;
            }
            EventSystem.Instance.PublishAsync(self.Root().CurrentScene(),new AdventureBattleReport()
            {
                Round = self.Round, BattleRoundResult = battleRoundResult
            }).Coroutine();
        }

        
        //有存活怪物返回第一个
        public static Unit GetTargetMonsterUnit(this AdventureComponent self)
        {
            self.AliveEnemyIdList.Clear();
            for (int i = 0; i < self.EnemyIdList.Count; i++)
            {
                Unit monsterUnit = self.Root().CurrentScene().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                if (monsterUnit.isAlive())
                {
                    self.AliveEnemyIdList.Add(monsterUnit.Id);
                }
            }

            if (self.AliveEnemyIdList.Count <= 0)
            {
                return null;
            }

            return self.Root().CurrentScene().GetComponent<UnitComponent>().Get(self.AliveEnemyIdList[0]);
        }

        public static BattleRoundResult GetBattleRoundResult(this AdventureComponent self)
        {
            Unit unit = UnitHelper.GetMyUnitFromCurrentScene(self.Root().CurrentScene());
            if (!unit.isAlive())
            {
                return BattleRoundResult.LoseBattle;
            }

            Unit monsterUnit = self.GetTargetMonsterUnit();
            if (monsterUnit == null)
            {
                return BattleRoundResult.WinBattle;
            }

            return BattleRoundResult.KeepBattle;
        }
    }
}
