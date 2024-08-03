namespace ET.Server
{
    public static class ItemFactory
    {
        [EnableAccessEntiyChild]
        public static Item Create( Entity self,int configId)
        {
            if (!ItemConfigCategory.Instance.Contain(configId))
            {
                Log.Error($"当前所创建的物品id 不存在 ：{configId}");
                return null;
            }

            Item item = self.AddChild<Item, int>(configId);
            item.RandomQuality();
            AddComponentByItemType(item);
            return item;
        }

        public static void AddComponentByItemType(Item item)
        {
            switch ((ItemType)item.Config.Type)
            {
                case ItemType.Weapon:
                case ItemType.Armor:
                case ItemType.Ring:
                {
                    item.AddComponent<EquipInfoComponent>();
                }
                    break;
                case ItemType.Prop:
                {
                    
                }
                    break;
                    
            }
        }
    }
}