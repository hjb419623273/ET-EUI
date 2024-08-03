namespace ET.Server
{
    [MessageLocationHandler(SceneType.Map)]
    public class C2M_SellItemHandler : MessageLocationHandler<Unit, C2M_SellItem, M2C_SellItem>
    {
        protected override async ETTask Run(Unit unit, C2M_SellItem request, M2C_SellItem response)
        {
            BagComponent bagComponent = unit.GetComponent<BagComponent>();

            if (!bagComponent.IsItemExist(request.ItemUid))
            {
                response.Error = ErrorCode.ERR_ItemNotExist;
                return;
            }

            Item bagItem = bagComponent.GetItemById(request.ItemUid);
            int addGold = bagItem.Config.SellBasePrice;
            
            bagComponent.RemoveItem(bagItem);
            unit.GetComponent<NumericComponent>()[NumericType.Coin] += addGold;

            await ETTask.CompletedTask;
        }
    }
}