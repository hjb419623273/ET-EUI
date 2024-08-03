namespace ET.Client
{
    public static class ItemFactory
    {
        [EnableAccessEntiyChild]
        public static Item Create(Entity self, int configId)
        {
            Item item = self.AddChild<Item, int>(configId);
            return item;
        }

        public static Item Create(Entity self, ItemInfo itemInfo)
        {
            // Item item = self?.AddChild<Item, int>(itemInfo.ItemConfigId);
            Item item = self?.AddChildWithId<Item, int>(itemInfo.ItemUid, itemInfo.ItemConfigId);
            item?.FromMessage(itemInfo);
            return item;
        }
    }
}