using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class ItemConfigCategory : Singleton<ItemConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, ItemConfig> dict = new();
		
        public void Merge(object o)
        {
            ItemConfigCategory s = o as ItemConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public ItemConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ItemConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ItemConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ItemConfig> GetAll()
        {
            return this.dict;
        }

        public ItemConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            
            var enumerator = this.dict.Values.GetEnumerator();
            enumerator.MoveNext();
            return enumerator.Current; 
        }
    }

	public partial class ItemConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>名字</summary>
		public string Name { get; set; }
		/// <summary>名字</summary>
		public string Desc { get; set; }
		/// <summary>物品类型</summary>
		public int Type { get; set; }
		/// <summary>装配位置</summary>
		public int EquipPosition { get; set; }
		/// <summary>c图标</summary>
		public string Icon { get; set; }
		/// <summary>最大累加数量</summary>
		public int MaxSumCount { get; set; }
		/// <summary>s词条随机Id</summary>
		public int EntryRandomId { get; set; }
		/// <summary>售卖的基础价格</summary>
		public int SellBasePrice { get; set; }

	}
}
