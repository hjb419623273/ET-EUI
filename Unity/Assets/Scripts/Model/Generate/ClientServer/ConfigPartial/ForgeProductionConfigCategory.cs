namespace ET
{
    public partial class ForgeProductionConfigCategory
    {
        //可制作物品数量
        public int GetProductionConfigCount(int unitLevel)
        {
            int count = 0;
            foreach (var config in this.dict.Values)
            {
                if (config.NeedLevel <= unitLevel)
                {
                    ++count;
                }
            }

            return count;
        }

        public ForgeProductionConfig GetProductionByLevelIndex(int unitLevel,int index)
        {
            int tempIndex = 0;
            foreach (var config in this.dict.Values)
            {
                if (config.NeedLevel <= unitLevel && index == tempIndex)
                {
                    return config;
                }

                if (config.NeedLevel <= unitLevel)
                {
                    ++tempIndex;
                }
            }

            return null;
        }
    }
}
