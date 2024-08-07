namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_UpdateTaskInfoHandler : MessageHandler<Scene, M2C_UpdateTaskInfo>
    {
        protected override async ETTask Run(Scene scene, M2C_UpdateTaskInfo message)
        {
            scene.Root().GetComponent<TasksComponent>().AddOrUpdateTaskInfo(message.TaskInfoProto);
            await ETTask.CompletedTask;
        }
    }
}