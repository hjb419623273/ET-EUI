using System.Collections.Generic;

namespace ET
{
    public partial class TaskConfigCategory
    {
        public Dictionary<int, List<int>> BeforeTaskConfigDictionary = new Dictionary<int, List<int>>();

        public List<int> GetAfterTaskIdListByBeforeId(int beforeConfigId)
        {
            this.ReCreateData();
            if (this.BeforeTaskConfigDictionary.TryGetValue(beforeConfigId, out List<int> configIdList))
            {
                return configIdList;
            }

            return null;
        }

        void ReCreateData()
        {
            if (BeforeTaskConfigDictionary.Count == 0)
            {
                foreach (var config in this.dict.Values)
                {
                    if (!this.BeforeTaskConfigDictionary.ContainsKey(config.TaskBeforeId))
                    {
                        this.BeforeTaskConfigDictionary.Add(config.TaskBeforeId, new List<int>());
                    }
                    this.BeforeTaskConfigDictionary[config.TaskBeforeId].Add(config.Id);
                }
            }
        }
    }
}