namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class UpdateChatInfoEvent_RefreshUI : AEvent<Scene, UpdateChatInfo>
    {
        protected override async ETTask Run(Scene scene, UpdateChatInfo args)
        {
            // scene.Root().GetComponent<UIComponent>()?.GetDlgLogic<DlgChat>()a
            await ETTask.CompletedTask;
        }
    }
}