namespace ET.Client
{
    public static class ItemHelper
    {
        public static void Clear(Scene scene, ItemContainerType itemContainerType)
        {
            if (itemContainerType == ItemContainerType.Bag)
            {
                scene?.GetComponent<BagComponent>()?.Clear();
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
                scene.GetComponent<EquipmentsComponent>()?.Clear();
            }
        }

        public static Item GetItem(Scene scene, long itemId, ItemContainerType itemContainerType)
        {
            if (itemContainerType == ItemContainerType.Bag)
            {
                return scene.GetComponent<BagComponent>().GetItemById(itemId);
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
                return scene.GetComponent<EquipmentsComponent>().GetItemById(itemId);
            }

            return null;
        }

        public static void AddItem(Scene scene, Item item, ItemContainerType itemContainerType)
        {
            if (itemContainerType == ItemContainerType.Bag)
            {
                scene.GetComponent<BagComponent>().AddItem(item);
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
                scene.GetComponent<EquipmentsComponent>().AddEquipItem(item);
            }
        }

        public static void RemoveItemById(Scene scene, long itemId, ItemContainerType itemContainerType)
        {
            Item item = GetItem(scene, itemId, itemContainerType);
            if (itemContainerType == ItemContainerType.Bag)
            {
                scene.GetComponent<BagComponent>().RemoveItem(item);
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
                scene.GetComponent<EquipmentsComponent>().UnloadEquipItem(item);
            }
        }

        public static void RemoveItem(Scene scene, Item item, ItemContainerType itemContainerType)
        {
            if (itemContainerType == ItemContainerType.Bag)
            {
                scene.GetComponent<BagComponent>().RemoveItem(item);
            }
            else if (itemContainerType == ItemContainerType.RoleInfo)
            {
                scene.GetComponent<EquipmentsComponent>().UnloadEquipItem(item);
            }
        }
    }
}