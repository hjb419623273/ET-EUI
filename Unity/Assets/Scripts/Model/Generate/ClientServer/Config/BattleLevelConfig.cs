using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class BattleLevelConfigCategory : Singleton<BattleLevelConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, BattleLevelConfig> dict = new();
		
        public void Merge(object o)
        {
            BattleLevelConfigCategory s = o as BattleLevelConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public BattleLevelConfig Get(int id)
        {
            this.dict.TryGetValue(id, out BattleLevelConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (BattleLevelConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, BattleLevelConfig> GetAll()
        {
            return this.dict;
        }

        public BattleLevelConfig GetOne()
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

	public partial class BattleLevelConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>怪物列表</summary>
		public int[] MonsterIds { get; set; }
		/// <summary>关卡名字</summary>
		public string Name { get; set; }
		/// <summary>准入等级范围</summary>
		public int[] MiniEnterLevel { get; set; }
		/// <summary>战斗胜利获取的经验值</summary>
		public int RewardExp { get; set; }

	}
}
