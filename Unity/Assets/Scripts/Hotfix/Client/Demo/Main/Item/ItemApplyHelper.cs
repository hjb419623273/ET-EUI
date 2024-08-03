using System;
using System.Security.Cryptography;

namespace ET.Client
{
    public static class ItemApplyHelper
    {
        //穿戴装备
        public static async ETTask<int> EquipItem(Scene scene, long itemId)
        {
            Item item = ItemHelper.GetItem(scene, itemId, ItemContainerType.Bag);
            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }

            M2C_EquipItem m2CEquipItem = null;

            try
            {
                C2M_EquipItem c2MEquipItem = C2M_EquipItem.Create();
                c2MEquipItem.ItemUid = itemId;
                m2CEquipItem = (M2C_EquipItem)await scene.Root().GetComponent<ClientSenderComponent>().Call(c2MEquipItem);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            return m2CEquipItem.Error;
        }
        
        //卸下装备
        public static async ETTask<int> UnloadEquipItem(Scene scene, long itemId)
        {
            Item item = ItemHelper.GetItem(scene, itemId, ItemContainerType.RoleInfo);
            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }

            M2C_UnloadEquipItem m2CUnloadEquipItem = null;

            try
            {
                C2M_UnloadEquipItem c2MUnloadEquipItem = C2M_UnloadEquipItem.Create();
                c2MUnloadEquipItem.EquipPosition = item.Config.EquipPosition;
                m2CUnloadEquipItem = (M2C_UnloadEquipItem)await scene.Root().GetComponent<ClientSenderComponent>().Call(c2MUnloadEquipItem);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            return m2CUnloadEquipItem.Error;
        }
        
        //售卖背包物品
        public static async ETTask<int> SellBagItem(Scene scene, long itemId)
        {
            Item item = ItemHelper.GetItem(scene, itemId, ItemContainerType.Bag);
            if (item == null)
            {
                return ErrorCode.ERR_ItemNotExist;
            }

            M2C_SellItem m2cSellItem = null;

            try
            {
                C2M_SellItem c2MSellItem = C2M_SellItem.Create();
                c2MSellItem.ItemUid = itemId;
                ClientSenderComponent clientSenderComponent = scene.GetComponent<ClientSenderComponent>();
                m2cSellItem = await clientSenderComponent.Call(c2MSellItem) as M2C_SellItem;
                // m2cSellItem = (M2C_SellItem)await scene.Root().GetComponent<ClientSenderComponent>().Call(c2MSellItem);
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            return m2cSellItem.Error;
        }
    }
}