using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class EntryConfigCategory : Singleton<EntryConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, EntryConfig> dict = new();
		
        public void Merge(object o)
        {
            EntryConfigCategory s = o as EntryConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public EntryConfig Get(int id)
        {
            this.dict.TryGetValue(id, out EntryConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (EntryConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, EntryConfig> GetAll()
        {
            return this.dict;
        }

        public EntryConfig GetOne()
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

	public partial class EntryConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>词条类型</summary>
		public int EntryType { get; set; }
		/// <summary>词条等级</summary>
		public int EntryLevel { get; set; }
		/// <summary>词条评分</summary>
		public int EntryScore { get; set; }
		/// <summary>属性类型</summary>
		public int AttributeType { get; set; }
		/// <summary>属性值最小范围</summary>
		public int AttributeMinValue { get; set; }
		/// <summary>属性值最大范围</summary>
		public int AttributeMaxValue { get; set; }

	}
}
