namespace ET.Server
{
    [Event(SceneType.Map)]
    public class MakeProdutionOverEvent_TaskUpdate : AEvent<Scene, MakeProdutionOver>
    {
        protected override async ETTask Run(Scene scene, MakeProdutionOver args)
        {
            args.Unit.GetComponent<TasksComponent>().TriggerTaskAction(TaskActionType.MakeItem,count:1,targetId : args.ProductionConfigId);
            await ETTask.CompletedTask;
        }
    }
}