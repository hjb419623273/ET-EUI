using System.Collections.Generic;
using System.Linq;

namespace ET
{
    public partial class BattleLevelConfigCategory
    {
        public BattleLevelConfig GetConfigByIndex(int index)
        {
            List<BattleLevelConfig> list = this.GetAll().Values.ToList();
            if (index < 0 || index >= list.Count)
            {
                Log.Error($"Get BattleLevelConfig Index Error: {index}");
                return null;
            }
            
            return list[index];
        }
    }
}