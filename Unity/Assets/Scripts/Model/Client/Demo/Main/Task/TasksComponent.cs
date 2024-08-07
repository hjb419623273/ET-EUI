using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class TasksComponent : Entity, IAwake, IDestroy
    {
        public SortedDictionary<int, EntityRef<TaskInfo>> TaskInfoDict = new SortedDictionary<int, EntityRef<TaskInfo>>();

        public List<EntityRef<TaskInfo>> TaskInfoList = new List<EntityRef<TaskInfo>>();
    }
}