namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class Event_RefreshItemPopUp: AEvent<Scene, RefreshItemPopUp>
    {
        protected override async ETTask Run(Scene scene, RefreshItemPopUp args)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgItemPopUp>()?.RefreshInfo(args.Item,args.ItemContainerType);
            await ETTask.CompletedTask;
        }
    }
}