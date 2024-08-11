using System.Linq;

namespace ET
{
    public partial class PlayerNumericConfigCategory
    {
        public int GetShowConfigCount()
        {
            return  this.dict.Values.Where(config => config.isNeedShow == 1).ToList().Count;
        }

        public PlayerNumericConfig GetConfigByIndex(int index)
        {
            int configIndex = 0;
            foreach (var info in this.dict)
            {
                if (info.Value.isNeedShow != 1)
                {
                  continue;
                }

                if (configIndex == index)
                {
                    return info.Value;
                }
                ++configIndex;
            }
            return null;
        }
        
    }
}