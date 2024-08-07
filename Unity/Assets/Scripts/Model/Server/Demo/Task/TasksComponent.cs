using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    [ComponentOf(typeof (Unit))]
    public class TasksComponent: Entity,IAwake,IDestroy,ITransfer,IUnitCache, IDeserialize
    {
        [BsonIgnore]
        public SortedDictionary<int,EntityRef<TaskInfo>> TaskInfoDict = new SortedDictionary<int, EntityRef<TaskInfo>>();

        [BsonIgnore]
        public HashSet<int> CurrentTaskSet = new HashSet<int>();
    }
}