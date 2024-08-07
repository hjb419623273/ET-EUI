using System;

namespace ET.Client
{
    public static class TaskHelper
    {
        public static async ETTask<int> GetTaskReward(Scene scene, int taskConfigId)
        {
            TaskInfo taskInfo = scene.GetComponent<TasksComponent>().GetTaskInfoByConfigId(taskConfigId);

            if ( taskInfo == null || taskInfo.IsDisposed )
            {
                return ErrorCode.ERR_NoTaskInfoExist;
            }

            if (!taskInfo.IsTaskState(TaskState.Complete))
            {
                return ErrorCode.ERR_TaskNoCompleted;
            }

            M2C_ReceiveTaskReward m2CReceiveTaskReward = null;
            try
            {
                C2M_ReceiveTaskReward c2MReceiveTaskReward = C2M_ReceiveTaskReward.Create();
                c2MReceiveTaskReward.TaskConfigId = taskConfigId;
                m2CReceiveTaskReward = await scene.GetComponent<ClientSenderComponent>().Call(c2MReceiveTaskReward) as M2C_ReceiveTaskReward;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            return m2CReceiveTaskReward.Error;
        }
    }
}