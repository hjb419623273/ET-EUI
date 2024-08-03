using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    [ComponentOf(typeof(Unit))]
    public class ForgeComponent: Entity,IAwake,IDestroy,ITransfer,IDeserialize,IUnitCache
    {                               
                              
        [BsonIgnore]
        public Dictionary<long, long> ProductionTimerDict = new Dictionary<long, long>();
        
        [BsonIgnore] 
        public List<EntityRef<Production>> ProductionsList = new List<EntityRef<Production>>();
    }
}