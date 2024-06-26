using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class PlayerNumericConfigCategory : Singleton<PlayerNumericConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, PlayerNumericConfig> dict = new();
		
        public void Merge(object o)
        {
            PlayerNumericConfigCategory s = o as PlayerNumericConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public PlayerNumericConfig Get(int id)
        {
            this.dict.TryGetValue(id, out PlayerNumericConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (PlayerNumericConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, PlayerNumericConfig> GetAll()
        {
            return this.dict;
        }

        public PlayerNumericConfig GetOne()
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

	public partial class PlayerNumericConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>名字</summary>
		public string Name { get; set; }
		/// <summary>初始基础值</summary>
		public long BaseValue { get; set; }
		/// <summary>是否用于展示</summary>
		public int isNeedShow { get; set; }
		/// <summary>是否用于加成点</summary>
		public int isAddPoint { get; set; }
		/// <summary>是否是百分比</summary>
		public int isPrecent { get; set; }

	}
}
