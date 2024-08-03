using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    [ComponentOf(typeof(Item))]
    public class EquipInfoComponent : Entity,IAwake,IDestroy,ISerializeToEntity,IDeserialize
    {
        public bool IsInited = false;
        
        //装备评分
        public int Score = 0;
        
        //装备词条列表
        [BsonIgnore]
        public List<EntityRef<AttributeEntry>> EntryList = new List<EntityRef<AttributeEntry>>();
    }
}

