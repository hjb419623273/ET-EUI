namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class Event_ReFreshMakeEqueue: AEvent<Scene, RefreshMakeEqueue>
    {
        protected override async ETTask Run(Scene scene, RefreshMakeEqueue args)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgForge>()?.RefreshMakeQueue();
            await ETTask.CompletedTask;
        }
    }
}