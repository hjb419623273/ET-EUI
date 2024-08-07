namespace ET.Server
{
    [EntitySystemOf(typeof(TaskInfo))]
    [FriendOfAttribute(typeof(ET.TaskInfo))]
    public static partial class TaskInfoSystem
    {
        [EntitySystem]
        private static void Awake(this ET.TaskInfo self, int configId)
        {
            self.ConfigId    = configId;
            self.TaskProgress = 0;
            self.TaskState   = (int)TaskState.Doing;
        }
        [EntitySystem]
        private static void Destroy(this ET.TaskInfo self)
        {
            self.ConfigId = 0;
            self.TaskProgress = 0;
            self.TaskState = (int)TaskState.None;
        }
        
        [EntitySystem]
        private static void Awake(this ET.TaskInfo self)
        {

        }
         public static void FromMessage(this TaskInfo self, TaskInfoProto taskInfoProto)
         {
             self.ConfigId = taskInfoProto.ConfigId;
             self.TaskProgress = taskInfoProto.TaskPogress;
             self.TaskState = taskInfoProto.TaskState;
         }
        
         public static TaskInfoProto ToMessage(this TaskInfo self)
         {
             TaskInfoProto TaskInfoProto = TaskInfoProto.Create();
             TaskInfoProto.ConfigId      = self.ConfigId;
             TaskInfoProto.TaskPogress   = self.TaskProgress;
             TaskInfoProto.TaskState     = self.TaskState;
             return TaskInfoProto;
         }
        
        public static void SetTaskState(this TaskInfo self, TaskState taskState)
        {
            self.TaskState = (int)taskState;
        }
        
        public static bool IsTaskState(this TaskInfo self, TaskState taskState)
        {
            return self.TaskState == (int)taskState;
        }

        public static void UpdateProgress(this TaskInfo self, int count)
        {
            var taskActionType = TaskConfigCategory.Instance.Get(self.ConfigId).TaskActionType;
            var config         = TaskActionConfigCategory.Instance.Get(taskActionType);
            if (config.TaskProgressType == (int)TaskProgressType.Add)
            {
                self.TaskProgress += count;
            }
            else if (config.TaskProgressType == (int)TaskProgressType.Sub)
            {
                self.TaskProgress -= count;
            }
            else if (config.TaskProgressType == (int)TaskProgressType.Update)
            {
                self.TaskProgress = count;
            }
        }      
        
        // 是否可以被完成
        public static void TryCompleteTask(this TaskInfo self)
        {
            if ( !self.IsCompleteProgress() || !self.IsTaskState(TaskState.Doing) )
            {
                return;
            }
            
            self.TaskState = (int)TaskState.Complete;
        }

        
        //是否达到任务目标数量
        public static bool IsCompleteProgress(this TaskInfo self)
        {
            return self.TaskProgress >= TaskConfigCategory.Instance.Get(self.ConfigId).TaskTargetCount;
        }

    }
}