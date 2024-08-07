namespace ET.Server
{
    [EntitySystemOf(typeof(TasksComponent))]
    [FriendOfAttribute(typeof(ET.Server.TasksComponent))]
    [FriendOfAttribute(typeof(ET.TaskInfo))]
    public static partial class TasksComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.TasksComponent self)
        {
            if (self.TaskInfoDict.Count == 0)
            {
                self.UpdateAfterTaskInfo(beforeTaskConfigId: 0, isNoticeClient: false);
            }
        }
        [EntitySystem]
        private static void Destroy(this ET.Server.TasksComponent self)
        {
            foreach (var taskInfo in self.TaskInfoDict.Values)
            {
                TaskInfo info = taskInfo;
                info?.Dispose();
            }
            self.TaskInfoDict.Clear();
            self.CurrentTaskSet.Clear(); ;
        }

        [EntitySystem]
        private static void Deserialize(this ET.Server.TasksComponent self)
        {
            foreach (Entity entity in self.Children.Values)
            {
                TaskInfo taskInfo = entity as TaskInfo;
                self.TaskInfoDict.Add(taskInfo.ConfigId, taskInfo);
                if (!taskInfo.IsTaskState(TaskState.Received))
                {
                    self.CurrentTaskSet.Add(taskInfo.ConfigId);
                }
            }
        }

        // 触发任务行为
        public static void TriggerTaskAction(this TasksComponent self, TaskActionType taskActionType, int count, int targetId = 0)
        {
            foreach (int taskConfigId in self.CurrentTaskSet)
            {
                TaskConfig taskConfig = TaskConfigCategory.Instance.Get(taskConfigId);
                if (taskConfig.TaskActionType == (int)taskActionType && taskConfig.TaskTargetId == targetId)
                {
                    self.AddOrUpdateTaskInfo(taskConfigId, count);
                }
            }
        }

        // 增加或者更新任务信息
        public static void AddOrUpdateTaskInfo(this TasksComponent self, int taskConfigId, int count, bool isNoticeClient = true)
        {
            if (!self.TaskInfoDict.TryGetValue(taskConfigId, out EntityRef<TaskInfo> taskInfo))
            {
                taskInfo = self.AddChild<TaskInfo, int>(taskConfigId);
                self.TaskInfoDict.Add(taskConfigId, taskInfo);
            }

            TaskInfo info = taskInfo;
            info.UpdateProgress(count);
            info.TryCompleteTask();
            if (isNoticeClient)
            {
                M2C_UpdateTaskInfo m2CUpdateTaskInfo = M2C_UpdateTaskInfo.Create();
                TaskNoticeHelper.SyncTaskInfo(self.GetParent<Unit>(), taskInfo, m2CUpdateTaskInfo);
            }
        }

        // 更新后续任务信息
        public static void UpdateAfterTaskInfo(this TasksComponent self, int beforeTaskConfigId, bool isNoticeClient = true)
        {
            self.CurrentTaskSet.Remove(beforeTaskConfigId);
            var taskConfigIdList = TaskConfigCategory.Instance.GetAfterTaskIdListByBeforeId(beforeTaskConfigId);
            if (taskConfigIdList == null)
            {
                return;
            }

            foreach (int taskConfigId in taskConfigIdList)
            {
                self.CurrentTaskSet.Add(taskConfigId);
                int count = self.GetTaskInitProgressCount(taskConfigId);
                self.AddOrUpdateTaskInfo(taskConfigId, count, isNoticeClient);
            }
        }


        public static int GetTaskInitProgressCount(this TasksComponent self, int taskConfigId)
        {
            var config = TaskConfigCategory.Instance.Get(taskConfigId);

            if (config.TaskActionType == (int)TaskActionType.UpLevel)
            {
                return self.GetParent<Unit>().GetComponent<NumericComponent>().GetAsInt(NumericType.Level);
            }
            return 0;
        }

        //领取任务状态
        public static void ReceiveTaskRewardState(this TasksComponent self, Unit unit, int taskConfigId)
        {
            if (!self.TaskInfoDict.TryGetValue(taskConfigId, out EntityRef<TaskInfo> taskInfo))
            {
                Log.Error($"TaskInfo Error :{taskConfigId}");
                return;
            }

            TaskInfo info = taskInfo;
            info.SetTaskState(TaskState.Received);
            M2C_UpdateTaskInfo m2CUpdateTaskInfo = M2C_UpdateTaskInfo.Create();
            TaskNoticeHelper.SyncTaskInfo(unit, taskInfo, m2CUpdateTaskInfo);
            self.UpdateAfterTaskInfo(taskConfigId);
        }

        // 是否可以领取任务奖励
        public static int TryReceiveTaskReward(this TasksComponent self, int taskConfigId)
        {
            if (!TaskConfigCategory.Instance.Contain(taskConfigId))
            {
                return ErrorCode.ERR_NoTaskExist;
            }

            self.TaskInfoDict.TryGetValue(taskConfigId, out EntityRef<TaskInfo> taskInfo);

            TaskInfo info = taskInfo;
            if (info == null || info.IsDisposed)
            {
                return ErrorCode.ERR_NoTaskInfoExist;
            }

            if (!self.IsBeforeTaskReceived(taskConfigId))
            {
                return ErrorCode.ERR_BeforeTaskNoOver;
            }

            if (info.IsTaskState(TaskState.Received))
            {
                return ErrorCode.ERR_TaskRewarded;
            }

            if (!info.IsTaskState(TaskState.Complete))
            {
                return ErrorCode.ERR_TaskNoCompleted;
            }

            return ErrorCode.ERR_Success;
        }
        // 前置任务是否完成并领取了奖励
        public static bool IsBeforeTaskReceived(this TasksComponent self, int taskConfigId)
        {
            var config = TaskConfigCategory.Instance.Get(taskConfigId);
            if (config.TaskBeforeId == 0)
            {
                return true;
            }

            if (!self.TaskInfoDict.TryGetValue(config.TaskBeforeId, out EntityRef<TaskInfo> beforeTaskInfo))
            {
                return false;
            }

            TaskInfo info = beforeTaskInfo;
            if (!info.IsTaskState(TaskState.Received))
            {
                return false;
            }

            return true;
        }
    }
}