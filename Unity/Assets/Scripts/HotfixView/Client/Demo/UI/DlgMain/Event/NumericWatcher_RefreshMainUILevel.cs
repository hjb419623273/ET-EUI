namespace ET.Client
{
    [NumericWatcher(SceneType.Current,NumericType.Level)]
    public class NumericWatcher_RefreshMainUIExp : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.Scene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.Refresh();
        }
    }
}
