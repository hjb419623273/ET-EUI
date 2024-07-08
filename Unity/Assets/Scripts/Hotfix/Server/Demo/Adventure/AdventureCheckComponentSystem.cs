using TrueSync;

namespace ET.Server
{
    [EntitySystemOf(typeof(AdventureCheckComponent))]
    [FriendOfAttribute(typeof(ET.Server.AdventureCheckComponent))]
    public static partial class AdventureCheckComponentSystem
    {

        [EntitySystem]
        private static void Awake(this ET.Server.AdventureCheckComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Server.AdventureCheckComponent self)
        {
            foreach (var mosterId in self.CacheEnemyIdList)
            {
                self.Root().GetComponent<UnitComponent>().Remove(mosterId);
            }
            self.CacheEnemyIdList.Clear();
            self.EnemyIdList.Clear();
            self.AniamtionTotalTime = 0;
            self.Random = null;
        }

        //
        public static bool CheckBattleWinResult(this AdventureCheckComponent self, int battleRound)
        {
            try
            {
                self.ResetAdventureInfo();
                self.SetBattleRandomSeed();
                self.CreateBattleMonsterUnit();
                
                NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();
                //模拟对战
                bool isSimulationNormal = self.SimulationBattle(battleRound);
                if (!isSimulationNormal)
                {
                    Log.Error("模拟对战失败");
                    return false;
                }
                
                //判断角色是否能在战斗中存活
                if (!self.GetParent<Unit>().isAlive())
                {
                    Log.Error("玩家未存活");
                    return false;
                }
                
                //判定所有怪物是否被击杀
                if (self.GetFirstAliveEnemy() != null)
                {
                    Log.Error("还有怪物存活");
                    return false;
                }
                
                //判断战斗动画时间是否正常
                long playAnimationTime = TimeInfo.Instance.ServerNow() - numericComponent.GetAsLong(NumericType.AdventureStartTime);
                if (playAnimationTime < self.AniamtionTotalTime)
                {
                    Log.Error("动画时间不足");
                    return false;
                }
                return true;
            }
            finally
            {
                self.ResetAdventureInfo();
            }
        }

        public static bool SimulationBattle(this AdventureCheckComponent self,int battleRound)
        {   
            for (int i = 0; i < battleRound; i++)
            {
                if (i % 2 == 0)
                {
                    Unit monsterUnit = self.GetFirstAliveEnemy();
                    if (monsterUnit == null)
                    {
                        Log.Debug("获取到怪物为空");
                        return false;
                    }

                    self.AniamtionTotalTime += 1000;
                    self.CalcuateDamageHpValue(self.GetParent<Unit>(), monsterUnit);
                }
                else
                {
                    if (!self.GetParent<Unit>().isAlive())
                    {
                        return false;
                    }

                    for (int j = 0; j < self.EnemyIdList.Count; j++)
                    {
                        Unit monsterUnit = self.Root().GetComponent<UnitComponent>().Get(self.EnemyIdList[j]);
                        if (!monsterUnit.isAlive())
                        {
                            continue;
                        }

                        self.AniamtionTotalTime += 1000;
                        self.CalcuateDamageHpValue(monsterUnit, self.GetParent<Unit>());
                    }
                }
            }

            return true;
        }

        //设置战斗随机数
        public static void SetBattleRandomSeed(this AdventureCheckComponent self)
        {
            int seed = self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.BattleRandomSeed);
            if (self.Random == null)
            {
                self.Random = TSRandom.New(seed);
            }
            else
            {
                self.Random.Initialize(seed);
            }
        }
        
        //创建关卡怪物Unit
        public static void CreateBattleMonsterUnit(this AdventureCheckComponent self)
        {
            int levelId = self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.AdventureState);
            //生成怪物最大数量
            BattleLevelConfig battleLevelConfig = BattleLevelConfigCategory.Instance.Get(levelId);
            int monsterCount = battleLevelConfig.MonsterIds.Length - self.CacheEnemyIdList.Count;
            for (int i = 0; i < monsterCount; i++)
            {
                Unit monsterUnit = UnitFactory.Create(self.Root(), 1002, UnitType.Monster);
                self.CacheEnemyIdList.Add(monsterUnit.Id);
            }
            //复用怪物Unit
            self.EnemyIdList.Clear();
            for (int i = 0; i < battleLevelConfig.MonsterIds.Length; i++)
            {
                Unit monsterUnit = self.Root().GetComponent<UnitComponent>().Get(self.CacheEnemyIdList[i]);
                UnitConfig unitConfig = UnitConfigCategory.Instance.Get(battleLevelConfig.MonsterIds[i]);
                monsterUnit.ConfigId = unitConfig.Id;

                NumericComponent numericComponent = monsterUnit.GetComponent<NumericComponent>();
                numericComponent.SetNoEvent(NumericType.MaxHp,monsterUnit.Config().MaxHP);
                numericComponent.SetNoEvent(NumericType.Hp,monsterUnit.Config().MaxHP);
                int damageValue = monsterUnit.Config().DamageValue;
                numericComponent.SetNoEvent(NumericType.DamageValue,damageValue);
                numericComponent.SetNoEvent(NumericType.IsAlive,1);
                self.EnemyIdList.Add(monsterUnit.Id);
            }
        }
        
        // 获取存活的怪物
        public static Unit GetFirstAliveEnemy(this AdventureCheckComponent self)
        {
               for (int i = 0; i < self.EnemyIdList.Count; i++)
               {
                   Unit monsterUnit = self.Root().GetComponent<UnitComponent>().Get(self.EnemyIdList[i]);
                   if (monsterUnit.isAlive())
                   {
                       return monsterUnit;
                   }
               }
               return null;
        }
        
        //计算伤害数值
        public static void CalcuateDamageHpValue(this AdventureCheckComponent self, Unit attackUnit, Unit targerUnit)
        {
            int Hp = targerUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Hp);
            Hp = Hp - DamageCalcuateHelper.CalculateDamageValue(attackUnit, targerUnit, ref self.Random);
            if (Hp < 0)
            {
                Hp = 0;
                targerUnit.GetComponent<NumericComponent>().SetNoEvent(NumericType.IsAlive, 0);
            }
            targerUnit.GetComponent<NumericComponent>().SetNoEvent(NumericType.Hp, Hp);
        }

        public static void ResetAdventureInfo(this AdventureCheckComponent self)
        {
            self.AniamtionTotalTime = 0;
            NumericComponent numericComponent = self.GetParent<Unit>().GetComponent<NumericComponent>();
            numericComponent.SetNoEvent(NumericType.Hp, numericComponent.GetAsInt(NumericType.MaxHp));
            numericComponent.SetNoEvent(NumericType.IsAlive, 1);
        }
    }
}