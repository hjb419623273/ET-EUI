using TrueSync;

namespace ET.Client
{
    [Event(SceneType.Current)]
    [FriendOfAttribute(typeof(ET.Client.AdventureComponent))]
    public class AdventureBattleRoundEvent_CalculateDamage : AEvent<Scene, AdventureBattleRound>
    {
        protected override async ETTask Run(Scene scene, AdventureBattleRound args)
        {
            if (!args.AttackUnit.isAlive() || !args.TargeUnit.isAlive())
            {
                return;
            }

            TSRandom random = scene.GetComponent<AdventureComponent>().Random;
            int damage = DamageCalcuateHelper.CalculateDamageValue(args.AttackUnit, args.TargeUnit, ref random);
            int Hp = args.TargeUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Hp);
            Hp -= damage;
            if (Hp <= 0)
            {
                Hp = 0;
                args.TargeUnit.SetAlive(false);
            }

            EventSystem.Instance.PublishAsync(scene, new ShowDamageValueView() { TargeUnit = args.TargeUnit, DamamgeValue = damage }).Coroutine();

            args.TargeUnit.GetComponent<NumericComponent>().Set(NumericType.Hp, Hp);
            Log.Debug($"************* {args.AttackUnit.Type()}攻击造成伤害:{damage} *************");
            Log.Debug($"************* {args.TargeUnit.Type()}被攻击剩余血量:{Hp} *************");
            await ETTask.CompletedTask;
        }
    }
}

