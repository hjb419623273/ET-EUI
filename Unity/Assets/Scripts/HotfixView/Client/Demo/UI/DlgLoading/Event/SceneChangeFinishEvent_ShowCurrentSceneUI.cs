namespace ET.Client
{
   [Event(SceneType.Current)]
   public class SceneChangeFinishEvent_ShowCurrentSceneUI: AEvent<Scene, SceneChangeFinish>
   {
      protected override async ETTask Run(Scene scene, SceneChangeFinish args)
      {
         scene.Root().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Loading);
         scene.Root().GetComponent<UIComponent>().ShowWindow(WindowID.WindowID_Main);
         await ETTask.CompletedTask;
      }
   } 
}