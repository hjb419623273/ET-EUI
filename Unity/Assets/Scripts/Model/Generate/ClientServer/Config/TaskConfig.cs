using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class TaskConfigCategory : Singleton<TaskConfigCategory>, IMerge
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, TaskConfig> dict = new();
		
        public void Merge(object o)
        {
            TaskConfigCategory s = o as TaskConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public TaskConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TaskConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (TaskConfig)}，配置id: {id}");
            }

            return item;
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TaskConfig> GetAll()
        {
            return this.dict;
        }

        public TaskConfig GetOne()
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

	public partial class TaskConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>任务名字</summary>
		public string TaskName { get; set; }
		/// <summary>任务描述</summary>
		public string TaskDesc { get; set; }
		/// <summary>任务行为类型</summary>
		public int TaskActionType { get; set; }
		/// <summary>任务目标Id</summary>
		public int TaskTargetId { get; set; }
		/// <summary>任务目标数量</summary>
		public int TaskTargetCount { get; set; }
		/// <summary>前置任务ID</summary>
		public int TaskBeforeId { get; set; }
		/// <summary>任务奖励金币数量</summary>
		public int RewardGoldCount { get; set; }

	}
}
