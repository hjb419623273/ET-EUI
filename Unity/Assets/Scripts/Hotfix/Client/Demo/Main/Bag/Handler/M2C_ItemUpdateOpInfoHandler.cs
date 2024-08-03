using ET.Server;

namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_ItemUpdateOpInfoHandler : MessageHandler<Scene, M2C_ItemUpdateOpInfo>
    {
        protected override async ETTask Run(Scene root, M2C_ItemUpdateOpInfo message)
        {
            //增加
            if (message.Op == (int)ItemOp.Add)
            {
                Item item = ItemFactory.Create(root, message.ItemInfo);
                ItemHelper.AddItem(root, item, (ItemContainerType)message.ContainerType);
            }
            else if (message.Op == (int) ItemOp.Remove)
            {
                ItemHelper.RemoveItemById(root, message.ItemInfo.ItemUid,(ItemContainerType)message.ContainerType);
            }

            await ETTask.CompletedTask;
        }
    }
}