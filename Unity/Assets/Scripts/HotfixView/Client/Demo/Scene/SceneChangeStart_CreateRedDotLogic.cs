namespace ET.Client
{
    [Event(SceneType.Demo)]
    public class SceneChangeStart_CreateRedDotLogic : AEvent<Scene, SceneChangeStart>
    {
        protected override async ETTask Run(Scene scene, SceneChangeStart args)
        {
            RedDotHelper.AddRedDotNode(scene,"Root", "Main" , false);
            RedDotHelper.AddRedDotNode(scene, "Main", "Role", false);
            RedDotHelper.AddRedDotNode(scene, "Role", "UpLevelButton", false);
            RedDotHelper.AddRedDotNode(scene, "Role", "AddAttribute", false);
            await ETTask.CompletedTask;
        }
    }
}