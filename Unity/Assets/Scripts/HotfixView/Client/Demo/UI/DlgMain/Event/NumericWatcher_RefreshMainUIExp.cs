namespace ET.Client
{
    [NumericWatcher(SceneType.Current,NumericType.Exp)]
    public class NumericWatcher_RefreshMainUILevel : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            unit.Scene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.Refresh();
        }
    }
}
