namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class AddAttributeFinish_RefreshEquipShowItems: AEvent<Scene, RefreshEquipShowItems>
    {
        protected override async ETTask Run(Scene scene, RefreshEquipShowItems a)
        {
            scene.GetComponent<UIComponent>().GetDlgLogic<DlgRoleInfo>()?.RefreshEquipShowItems();
            await ETTask.CompletedTask;
        }
    }
}