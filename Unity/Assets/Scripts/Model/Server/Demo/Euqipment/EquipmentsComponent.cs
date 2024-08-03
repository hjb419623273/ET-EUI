using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    [ComponentOf(typeof(Unit))]
    public class EquipmentsComponent : Entity,IAwake,IDestroy,ITransfer,IDeserialize,IUnitCache
    {
        [BsonIgnore]
        public Dictionary<int, EntityRef<Item>> EquipItems = new Dictionary<int, EntityRef<Item>>();
        //
        // [BsonIgnore]
        // public M2C_ItemUpdateOpInfo message = M2C_ItemUpdateOpInfo.Create();
    }
}