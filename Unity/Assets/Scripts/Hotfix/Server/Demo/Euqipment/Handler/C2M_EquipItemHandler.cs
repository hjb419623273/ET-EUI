namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_EquipItemHandler : MessageLocationHandler<Unit, C2M_EquipItem, M2C_EquipItem>
    {
        protected override async ETTask Run(Unit unit, C2M_EquipItem request, M2C_EquipItem response)
        {
            BagComponent bagComponent = unit.GetComponent<BagComponent>();
            EquipmentsComponent equipmentsComponent = unit.GetComponent<EquipmentsComponent>();
            if (!bagComponent.IsItemExist(request.ItemUid))
            {
                response.Error = ErrorCode.ERR_ItemNotExist;
                return;
            }

            Item bagItem = bagComponent.GetItemById(request.ItemUid);
            var equipPosition = (EquipPosition)bagItem.Config.EquipPosition;
            bagItem = bagComponent.RemoveItemNoDispose(bagItem);

            Item equipItem = equipmentsComponent.GetEquipItemByPosition(equipPosition);
            if (equipItem != null)
            {
                if (!bagComponent.IsCanAddItem(equipItem))
                {
                    bagComponent.AddItem(bagItem);
                    response.Error = ErrorCode.ERR_AddBagItemError;
                    return;
                }

                equipItem = equipmentsComponent.UnloadEquipItemByPosition(equipPosition);
                bagComponent.AddItem(equipItem);
            }

            if (!equipmentsComponent.EquipItem(bagItem))
            {
                response.Error = ErrorCode.ERR_EquipItemError;
                return;
            }

            await ETTask.CompletedTask;
        }
    }
}