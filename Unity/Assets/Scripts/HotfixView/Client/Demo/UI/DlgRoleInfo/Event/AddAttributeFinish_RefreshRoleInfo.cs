namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class AddAttributeFinish_RefreshRoleInfo : AEvent<Scene, RefreshRoleInfo>
    {
        protected override async ETTask Run(Scene scene, RefreshRoleInfo a)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>()?.Refresh();
            await ETTask.CompletedTask;
        }
    }
}

