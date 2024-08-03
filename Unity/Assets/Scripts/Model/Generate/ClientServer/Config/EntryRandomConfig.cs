using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class EntryRandomConfigCategory : Singleton<EntryRandomConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, EntryRandomConfig> dict = new();
		
        public void Merge(object o)
        {
            EntryRandomConfigCategory s = o as EntryRandomConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public EntryRandomConfig Get(int id)
        {
            this.dict.TryGetValue(id, out EntryRandomConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (EntryRandomConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, EntryRandomConfig> GetAll()
        {
            return this.dict;
        }

        public EntryRandomConfig GetOne()
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

	public partial class EntryRandomConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>随机词条最小数量</summary>
		public int EntryRandMinCount { get; set; }
		/// <summary>随机词条最大数量</summary>
		public int EntryRandMaxCount { get; set; }
		/// <summary>随机特殊词条最小数量</summary>
		public int SpecialEntryRandMinCount { get; set; }
		/// <summary>随机特殊词条最大数量</summary>
		public int SpecialEntryRandMaxCount { get; set; }
		/// <summary>随机词条等级</summary>
		public int EntryLevel { get; set; }
		/// <summary>随机特殊词条等级</summary>
		public int SpecialEntryLevel { get; set; }

	}
}
