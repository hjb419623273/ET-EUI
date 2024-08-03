using System.Collections.Generic;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.BagComponent))]
    [FriendOfAttribute(typeof(ET.Client.EquipmentsComponent))]
    [FriendOfAttribute(typeof(ET.Server.EquipmentsComponent))]
    public static class ItemUpdateNoticeHelper
    {
        public static void SyncAddItem(Unit unit, Item item, M2C_ItemUpdateOpInfo message)
        {
            message.ItemInfo = item.ToMessage();
            message.Op = (int)ItemOp.Add;
            MapMessageHelper.SendToClient(unit, message);
        }

        public static void SyncRemoveItem(Unit unit, Item item, M2C_ItemUpdateOpInfo message)
        {
            message.ItemInfo = item.ToMessage(false);
            message.Op = (int)ItemOp.Remove;
            MapMessageHelper.SendToClient(unit, message);
        }

        public static void SyncAllBagItem(Unit unit)
        {
            M2C_AllItemsList m2CAllItemsList = M2C_AllItemsList.Create();
            m2CAllItemsList.ContainerType = (int)ItemContainerType.Bag;
            m2CAllItemsList.ItemInfoList = new List<ItemInfo>();
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            foreach (EntityRef<Item> entityRefItem in bagComponent.ItemsDict.Values)
            {
                Item item = entityRefItem;
                m2CAllItemsList.ItemInfoList.Add(item.ToMessage());
            }
            MapMessageHelper.SendToClient(unit, m2CAllItemsList);
        }

        public static void SyncAllEquipItems(Unit unit)
        {
            M2C_AllItemsList m2CAllItemsList = M2C_AllItemsList.Create();
            m2CAllItemsList.ContainerType = (int)ItemContainerType.RoleInfo;
            m2CAllItemsList.ItemInfoList = new List<ItemInfo>();
            EquipmentsComponent equipmentsComponent = unit.GetComponent<EquipmentsComponent>();
            foreach (Item item in equipmentsComponent.EquipItems.Values)
            {
                m2CAllItemsList.ItemInfoList.Add(item.ToMessage());
            }
            MapMessageHelper.SendToClient(unit, m2CAllItemsList);
        }
    }
}