using System.Drawing.Imaging;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.TasksComponent))]
    public static  class TaskNoticeHelper
    {
        public static void SyncTaskInfo(Unit unit, TaskInfo taskInfo, M2C_UpdateTaskInfo updateTaskInfo)
        {
            updateTaskInfo.TaskInfoProto = taskInfo.ToMessage();
            MapMessageHelper.SendToClient(unit, updateTaskInfo);
        }

        public static void SyncAllTaskInfo(Unit unit)
        {
            TasksComponent tasksComponent = unit.GetComponent<TasksComponent>();

            M2C_AllTaskInfoList m2CAllTaskInfoList = M2C_AllTaskInfoList.Create();
            foreach (TaskInfo taskInfo in tasksComponent.TaskInfoDict.Values)
            {
                m2CAllTaskInfoList.TaskInfoProtoList.Add(taskInfo.ToMessage());
            }
            MapMessageHelper.SendToClient(unit, m2CAllTaskInfoList);
        }
    }
}