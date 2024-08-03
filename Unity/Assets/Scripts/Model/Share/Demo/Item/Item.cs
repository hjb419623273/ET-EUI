using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
   [ChildOf]   //https://et-framework.cn/d/1961-childof-componentof
   public class Item : Entity,IAwake<int>,IDestroy,ISerializeToEntity
   {
      //物品配置id
      public int ConfigId = 0;
      
      //物品品质
      public int Quality  = 0;
      
      //物品配置数据
      [BsonIgnore]
      public ItemConfig Config => ItemConfigCategory.Instance.Get(ConfigId);
   } 
}