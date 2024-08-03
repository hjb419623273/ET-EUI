namespace ET.Server
{
    [EntitySystemOf(typeof(EquipmentsComponent))]
    [FriendOfAttribute(typeof(ET.Server.EquipmentsComponent))]
    public static partial class EquipmentsComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.EquipmentsComponent self)
        {
       
        }
        [EntitySystem]
        private static void Destroy(this ET.Server.EquipmentsComponent self)
        {
            foreach (Item item in self.EquipItems.Values)
            {
                item?.Dispose();
            }
            self.EquipItems.Clear();
        }
        [EntitySystem]
        private static void Deserialize(this ET.Server.EquipmentsComponent self)
        {
            foreach (var entity in self.Children.Values)
            {
                Item item = entity as Item;
                self.EquipItems.Add(item.Config.EquipPosition,item);
            }
        }
        
        // 对应位置处是否有装配Item
        public static bool IsEquipItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            self.EquipItems.TryGetValue((int)equipPosition, out EntityRef<Item> item);
            Item ent = item;
            return ent != null && !ent.IsDisposed;
        }
        
        // 装配Item
        public static bool EquipItem(this EquipmentsComponent self, Item item)
        {
            if (!self.EquipItems.ContainsKey(item.Config.EquipPosition))
            {
                if (item.Parent != self)
                {
                    self.AddChild(item);
                }
                self.EquipItems.Add(item.Config.EquipPosition,item);
                EventSystem.Instance.Publish(self.Root(),new ChangeEquipItem(){Unit = self.GetParent<Unit>(),Item = item,EquipOp = EquipOp.Load});
                
                M2C_ItemUpdateOpInfo message = M2C_ItemUpdateOpInfo.Create();
                message.ContainerType = (int)ItemContainerType.RoleInfo;
                ItemUpdateNoticeHelper.SyncAddItem(self.GetParent<Unit>(),item,message);
                
                return true;
            }
            return false;
        }
        
        //卸下item
        public static Item UnloadEquipItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            if (self.EquipItems.TryGetValue((int)equipPosition, out EntityRef<Item> item))
            {
                self.EquipItems.Remove((int)equipPosition);
                EventSystem.Instance.Publish(self.Root(),new ChangeEquipItem(){Unit = self.GetParent<Unit>(),Item = item,EquipOp = EquipOp.Unload});
                
                M2C_ItemUpdateOpInfo message = M2C_ItemUpdateOpInfo.Create();
                message.ContainerType = (int)ItemContainerType.RoleInfo;
                ItemUpdateNoticeHelper.SyncRemoveItem(self.GetParent<Unit>(), item, message);
            }

            return item;
        }

        public static Item GetEquipItemByPosition(this EquipmentsComponent self, EquipPosition equipPosition)
        {
            if (!self.EquipItems.TryGetValue((int) equipPosition,out EntityRef<Item> item))
            {
                return null;
            }
            return item;
        }
    }
}