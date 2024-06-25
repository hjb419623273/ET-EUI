namespace ET.Server
{
   [Event(SceneType.Map)]
   public class NumericChangeEvent_NoticeClient: AEvent<Scene,NumbericChange>
   {
      protected override async ETTask Run(Scene scene, NumbericChange a)
      {
         NumbericChange args = (NumbericChange)a;
         if (args.Unit is not Unit unit)
         {
            return;
         }

         if (unit.Type() != UnitType.Player)
         {
            return;
         }

         unit.GetComponent<NumericNoticeComponent>()?.NoticeImmediately(args);
         await ETTask.CompletedTask;
      }
   } 
}