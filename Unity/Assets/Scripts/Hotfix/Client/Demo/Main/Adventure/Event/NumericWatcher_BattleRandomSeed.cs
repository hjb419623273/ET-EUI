namespace ET.Client
{
    [NumericWatcher(SceneType.Current,NumericType.BattleRandomSeed)]
    public class NumericWatcher_BattleRandomSeed : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (unit == null || unit.IsDisposed)
            {
                return;
            }
            unit.Root().CurrentScene().GetComponent<AdventureComponent>()?.SetBattleRandomSeed();
        }
    }
}

