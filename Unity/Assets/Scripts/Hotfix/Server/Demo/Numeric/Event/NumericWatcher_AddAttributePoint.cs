namespace ET.Server
{
    [NumericWatcher(SceneType.Map,NumericType.Spirit)]
    public class NumericWatcher_AddAttributePoint : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (!(args.Unit is Unit))
            {
                return;
            }
      
            //精神+1点 最大法力值 +1%
            if (args.NumericType == NumericType.Spirit)
            {
                unit.GetComponent<NumericComponent>()[NumericType.MaxMpFinalPct] += 1 * 10000;
            }
        }
    }
    
    [NumericWatcher(SceneType.Map, NumericType.Power)]
    public partial class NumericWatcher_AddAttributePoint1 : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (!(args.Unit is Unit))
            {
                return;
            }

            //力量+1点 伤害值+5
            if (args.NumericType == NumericType.Power)
            {
                unit.GetComponent<NumericComponent>()[NumericType.DamageValueAdd] += 5;
            }
        }

    }

    [NumericWatcher(SceneType.Map,NumericType.PhysicalStrength)]
    public partial class NumericWatcher_AddAttributePoint2 : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (!(args.Unit is Unit))
            {
                return;
            } 
            //体力+1点 最大生命值 +1%
            if (args.NumericType == NumericType.PhysicalStrength)
            {
                unit.GetComponent<NumericComponent>()[NumericType.MaxHpPct] += 1 * 10000;
            }

        }
    }

    [NumericWatcher(SceneType.Map,NumericType.Agile)]
    public partial class NumericWatcher_AddAttributePoint3 : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (!(args.Unit is Unit))
            {
                return;
            }


            //敏捷+1点  闪避概率加0.1%
            if (args.NumericType == NumericType.Agile)
            {
                // unit.GetComponent<NumericComponent>()[NumericType.DodgeFinalAdd] += 1 * 1000;
            } 
        }
    }
}
