using TrueSync;

namespace ET.Client
{
    public static partial class DamageCalcuateHelper
    {
        public static int CalculateDamageValue(Unit attackUnit, Unit TargeUnit, ref TSRandom random)
        {
            int damage = attackUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.DamageValue);
            int dodge = TargeUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Dodge);
            int armor = TargeUnit.GetComponent<NumericComponent>().GetAsInt(NumericType.Armor);

            int rate = random.Range(0, 1000000);
            Log.Debug("Rate : " + rate.ToString());
            if (rate < dodge)
            {
                //躲避成功
                Log.Debug("躲避成功");
                damage = 0;
            }

            if (damage > 0)
            {
                //扣掉护甲值
                damage = damage - armor;
                //造成最小的1点伤害值
                damage = damage <= 0 ? 1 : damage;
            }

            return damage;
        }
    }
}