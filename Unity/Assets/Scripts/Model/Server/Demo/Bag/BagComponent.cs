using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    [ComponentOf(typeof (Unit))]
    //IDeserialize需要实现反序列化生命周期函数 ITransfer可以随着unit传送例如从gate传送到map IUnitCache可落地到数据库中
    public class BagComponent : Entity, IAwake, IDestroy, IDeserialize,ITransfer, IUnitCache     
    {
        //BsonIgnore 不会落地到数据库中标签
        [BsonIgnore]
        public Dictionary<long, EntityRef<Item>> ItemsDict = new Dictionary<long, EntityRef<Item>>(); 
        
        [BsonIgnore]
        public MultiMap<int, EntityRef<Item>> ItemsMap = new MultiMap<int, EntityRef<Item>>();//允许重复键      
        
        // [BsonIgnore]
        // public M2C_ItemUpdateOpInfo message = M2C_ItemUpdateOpInfo.Create();
    }
}