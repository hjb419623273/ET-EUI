namespace ET.Client
{
    [NumericWatcher(SceneType.Current,NumericType.Exp)]

    public class NumericWathcer_AddExp: INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (unit == null || unit.IsDisposed)
            {
                return;
            }

            NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
            int unitLevel = numericComponent.GetAsInt(NumericType.Level);
            if (PlayerLevelConfigCategory.Instance.Contain(unitLevel))
            {
                long needExp = PlayerLevelConfigCategory.Instance.Get(unitLevel).NeedExp;
                if (args.New >= needExp)
                {
                    RedDotHelper.ShowRedDotNode(unit.Root(), "UpLevelButton");
                }
                else
                {
                    if (RedDotHelper.IsLogicAlreadyShow(unit.Root(), "UpLevelButton"))
                    {
                        RedDotHelper.HideRedDotNode(unit.Root(), "UpLevelButton");
                    }
                }                
            }
            
            unit.Scene().GetComponent<UIComponent>().GetDlgLogic<DlgMain>()?.Refresh();
        }
    }
    
    
    [NumericWatcher(SceneType.Current,NumericType.AttributePoint)]
    public class NumericWathcer_AttributePoint: INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (args.New > 0)
            {
                RedDotHelper.ShowRedDotNode(unit.Root(), "AddAttribute");
            }
            else
            {
                if (RedDotHelper.IsLogicAlreadyShow(unit.Root(), "AddAttribute"))
                {
                    RedDotHelper.HideRedDotNode(unit.Root(), "AddAttribute");
                }
            }
            unit.Scene().GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>()?.Refresh();
        }
    }
}
