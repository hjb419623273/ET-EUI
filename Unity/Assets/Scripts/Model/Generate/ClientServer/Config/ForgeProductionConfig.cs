using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class ForgeProductionConfigCategory : Singleton<ForgeProductionConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, ForgeProductionConfig> dict = new();
		
        public void Merge(object o)
        {
            ForgeProductionConfigCategory s = o as ForgeProductionConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public ForgeProductionConfig Get(int id)
        {
            this.dict.TryGetValue(id, out ForgeProductionConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (ForgeProductionConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, ForgeProductionConfig> GetAll()
        {
            return this.dict;
        }

        public ForgeProductionConfig GetOne()
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

	public partial class ForgeProductionConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>物品Id</summary>
		public int ItemConfigId { get; set; }
		/// <summary>制造时间</summary>
		public int ProductionTime { get; set; }
		/// <summary>制造所需的资源Id</summary>
		public int ConsumId { get; set; }
		/// <summary>制造所需的资源数量</summary>
		public int ConsumeCount { get; set; }
		/// <summary>制造所需等级</summary>
		public int NeedLevel { get; set; }

	}
}
