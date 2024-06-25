namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_TestUnitNumericHandler : MessageLocationHandler<Unit, C2M_TestUnitNumeric, M2C_TestUnitNumeric>
    {
        protected override async ETTask Run(Unit unit, C2M_TestUnitNumeric request, M2C_TestUnitNumeric response)
        {
            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();

            int newCoin = numericComponent.GetAsInt(NumericType.Coin) + 100;
            long newExp = numericComponent.GetAsLong(NumericType.Exp) + 50;
            long newLevel = numericComponent.GetAsLong(NumericType.Level) + 1;
            numericComponent.Set(NumericType.Coin, newCoin);
            numericComponent.Set(NumericType.Exp, newExp);
            numericComponent.Set(NumericType.Level, newLevel);

            await ETTask.CompletedTask;
        }
    }
}

