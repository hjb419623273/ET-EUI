using System.Collections.Generic;

namespace ET.Server
{
    [EntitySystemOf(typeof(BagComponent))]
    [FriendOfAttribute(typeof(ET.Server.BagComponent))]
    [FriendOfAttribute(typeof(ET.Item))]
    public static partial class BagComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.BagComponent self)
        {
        }
        [EntitySystem]
        private static void Destroy(this ET.Server.BagComponent self)
        {
            foreach (Item item in self.ItemsDict.Values)
            {
                item?.Dispose();
            }
            self.ItemsDict.Clear();
            self.ItemsMap.Clear();
        }
        
        //反序列化
        [EntitySystem]
        private static void Deserialize(this ET.Server.BagComponent self)
        {
            foreach (Entity entity in self.Children.Values)
            {
                self.AddContainer(entity as Item);
            }
        }

        public static bool IsMaxLoad(this BagComponent self)
        {
            return self.ItemsDict.Count == self.GetParent<Unit>().GetComponent<NumericComponent>()[NumericType.MaxBagCapacity];
        }

        public static bool AddContainer(this BagComponent self, Item item)
        {
            if (self.ItemsDict.ContainsKey(item.Id))
            {
                return false;
            }

            self.ItemsDict.Add(item.Id, item);
            self.ItemsMap.Add(item.Config.Type, item);
            return true;
        }

        public static void RemoveContainer(this BagComponent self, Item item)
        {
            self.ItemsDict.Remove(item.Id);
            self.ItemsMap.Remove(item.Config.Type, item);
        }

        public static bool AddItemByConfigId(this BagComponent self, int configId, int count = 1)
        {
            if (!ItemConfigCategory.Instance.Contain(configId))
            {
                return false;
            }

            if (count < 0)
            {
                return false;
            }

            for (int i = 0; i < count; i++)
            {
                Item newItem = ItemFactory.Create(self, configId);
                if (!self.AddItem(newItem))
                {
                    Log.Error("添加物品失败");
                    newItem?.Dispose();
                    return false;
                }
            }

            return true;
        }

        public static void GetItemListByConfigId(this BagComponent self, int configID, List<Item> list)
        {
            ItemConfig itemConfig = ItemConfigCategory.Instance.Get(configID);
            foreach (EntityRef<Item> goods in self.ItemsMap[itemConfig.Type])
            {
                Item ent = goods;
                if (ent.ConfigId == configID)
                {
                    list.Add(ent);
                }
            }
        }

        public static bool IsCanAddItem(this BagComponent self, Item item)
        {
            if (item == null || item.IsDisposed)
            {
                return false;
            }

            if (!ItemConfigCategory.Instance.Contain(item.ConfigId))
            {
                return false;
            }

            if (self.IsMaxLoad())
            {
                return false;
            }

            if (self.ItemsDict.ContainsKey(item.Id))
            {
                return false;
            }

            if (item.Parent == self)
            {
                return false;
            }

            return true;
        }

        public static bool IsCanAddItemByConfigId(this BagComponent self, int configID)
        {
            if (!ItemConfigCategory.Instance.Contain(configID))
            {
                return false;
            }

            if (self.IsMaxLoad())
            {
                return false;
            }

            return true;
        }

        public static bool IsCanAddItemList(this BagComponent self, List<BagComponent> goodsList)
        {
            if (goodsList.Count < 0)
            {
                return false;
            }

            if (self.ItemsDict.Count + goodsList.Count > self.GetParent<Unit>().GetComponent<NumericComponent>()[NumericType.MaxBagCapacity])
            {
                return false;
            }

            foreach (var item in goodsList)
            {
                if (item == null || item.IsDisposed)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool AddItemList(this BagComponent self, List<Item> itemsList)
        {
            if (itemsList.Count <= 0)
            {
                return true;
            }

            foreach (Item newItem in itemsList)
            {
                if (!self.AddItem(newItem))
                {
                    newItem?.Dispose();
                    return false;
                }
            }

            return true;
        }

        public static bool AddItem(this BagComponent self, Item item)
        {
            if (item == null || item.IsDisposed)
            {
                Log.Error("item is null");
                return false;
            }

            if (self.IsMaxLoad())
            {
                Log.Error("bag is IsMaxLoad!");
                return false;
            }
            
            if ( !self.AddContainer(item) )
            {
                Log.Error("Add Container is Error!");
                return false;
            }

            if (item.Parent != self)
            {
                //这里设置parent结构改变可以研究下  继承ISerializeToEntity的Entity可以落地缓存服和数据库
                //需要仔细分析下Entity.cs
                self.AddChild(item);
            }

            M2C_ItemUpdateOpInfo message = M2C_ItemUpdateOpInfo.Create();
            message.ContainerType = (int)ItemContainerType.Bag;

            //发送消息给Client
            ItemUpdateNoticeHelper.SyncAddItem(self.GetParent<Unit>(), item, message);
            return true;
        }

        public static void RemoveItem(this BagComponent self, Item item)
        {
            self.RemoveContainer(item);
            M2C_ItemUpdateOpInfo message = M2C_ItemUpdateOpInfo.Create();
            message.ContainerType = (int)ItemContainerType.Bag;
            //发送消息给Client
            ItemUpdateNoticeHelper.SyncRemoveItem(self.GetParent<Unit>(), item, message);
            item.Dispose();
        }

        public static Item RemoveItemNoDispose(this BagComponent self, Item item)
        {
            self.RemoveContainer(item);
            M2C_ItemUpdateOpInfo message = M2C_ItemUpdateOpInfo.Create();
            message.ContainerType = (int)ItemContainerType.Bag;
            //发送消息给client
            ItemUpdateNoticeHelper.SyncRemoveItem(self.GetParent<Unit>(), item, message);
            return item;
        }

        public static bool IsItemExist(this BagComponent self, long itemId)
        {
            self.ItemsDict.TryGetValue(itemId, out EntityRef<Item> item);
            Item ent = item;
            return ent != null && !ent.IsDisposed;
        }

        public static Item GetItemById(this BagComponent self, long itemId)
        {
            self.ItemsDict.TryGetValue(itemId, out EntityRef<Item> item);
            Item ent = item;
            return ent;
        }
    }
}