namespace ET.Client
{
    [FriendOfAttribute(typeof(ET.Client.BagComponent))]
    [EntitySystemOf(typeof(BagComponent))]
    public static partial class BagComponentSystem
    {
        //是否达到最大负载
        public static bool IsMaxLoad(this BagComponent self)
        {
            NumericComponent numericComponent = UnitHelper.GetMyUnitNumericComponent(self.Root().CurrentScene());
            return self.ItemsDict.Count == numericComponent[NumericType.MaxBagCapacity];
        }

        public static void Clear(this BagComponent self)
        {
            foreach (var item in self.ItemsDict)
            {
                long key = item.Key;
                Item value = item.Value;
                value?.Dispose();
            }
            self.ItemsDict.Clear();
            self.ItemsMap.Clear();
        }

        public static int GetItemCountByItemType(this BagComponent self, ItemType itemType)
        {
            if (!self.ItemsMap.ContainsKey((int)itemType))
            {
                return 0;
            }

            return self.ItemsMap[(int)itemType].Count;
        }

        public static void AddItem(this BagComponent self, Item item)
        {
            self.AddChild(item);
            self.ItemsDict.Add(item.Id, item);
            self.ItemsMap.Add(item.Config.Type, item);
        }

        public static void RemoveItem(this BagComponent self, Item item)
        {
            if (item == null)
            {
                Log.Error("bag item is null");
                return;
            }

            self.ItemsDict.Remove(item.Id);
            self.ItemsMap.Remove(item.Config.Type, item);
            item?.Dispose();
        }

        public static Item GetItemById(this BagComponent self, long itemId)
        {
            if (self.ItemsDict.TryGetValue(itemId, out EntityRef<Item> item))
            {
                Item ent = item;
                return ent;
            }

            return null;
        }
        
        [EntitySystem]
        private static void Awake(this ET.Client.BagComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.BagComponent self)
        {
            self.Clear();
        }
    }
}