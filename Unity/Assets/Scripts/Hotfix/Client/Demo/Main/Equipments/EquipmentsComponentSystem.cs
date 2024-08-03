namespace ET.Client
{
    [EntitySystemOf(typeof(EquipmentsComponent))]
    [FriendOfAttribute(typeof(ET.Client.EquipmentsComponent))]
    public static partial class EquipmentsComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.EquipmentsComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.EquipmentsComponent self)
        {
            self.Clear();
        }

        public static void Clear(this EquipmentsComponent self)
        {
            foreach (var item in self.EquipItems)
            {
                Item itemTmp = item.Value;
                itemTmp?.Dispose();
            }
            self.EquipItems.Clear();
        }

        public static Item GetItemById(this EquipmentsComponent self, long itemId)
        {
            if (self.Children.TryGetValue(itemId, out Entity entity))
            {
                return entity as Item;
            }

            return null;
        }

        public static Item GetItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            if (self.EquipItems.TryGetValue((int)equipPosition, out EntityRef<Item> item))
            {
                return item;
            }

            return null;
        }

        public static void AddEquipItem(this EquipmentsComponent self, Item item)
        {
            if (self.EquipItems.TryGetValue(item.Config.EquipPosition, out EntityRef<Item>  equipItem))
            {
                Log.Error($"Already EquipItem in Postion{(EquipPosition) item.Config.EquipPosition}");
                return;
            }

            self.AddChild(item);
            self.EquipItems.Add(item.Config.EquipPosition, item);
        }

        public static bool IsEquipItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            return self.EquipItems.ContainsKey((int)equipPosition);
        }

        public static bool UnloadEquipItem(this EquipmentsComponent self, Item item)
        {
            self.EquipItems.Remove(item.Config.EquipPosition);
            item?.Dispose();
            // self.RemoveChild(item.Id);
            return true;
        }
        
    }
}