namespace ET.Client
{
    [NumericWatcher(SceneType.Current,NumericType.Coin)]
    public class NumericWatcher_RefreashMainUICoin : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.Root().GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.Refresh();
        }
    }
}
