namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_AllTaskInfoListHandler : MessageHandler<Scene, M2C_AllTaskInfoList>
    {
        protected override async ETTask Run(Scene scene, M2C_AllTaskInfoList message)
        {
            if (message.TaskInfoProtoList != null)
            {
                foreach (var taskInfoProto in message.TaskInfoProtoList)
                {
                    scene.Root().GetComponent<TasksComponent>().AddOrUpdateTaskInfo(taskInfoProto);
                }
            }

            await ETTask.CompletedTask;
        }
    } 
}