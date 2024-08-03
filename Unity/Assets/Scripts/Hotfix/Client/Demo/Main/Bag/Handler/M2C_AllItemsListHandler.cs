namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_AllItemsListHandler : MessageHandler<Scene, M2C_AllItemsList>
    {
        protected override async ETTask Run(Scene scene, M2C_AllItemsList message)
        {
            ItemHelper.Clear(scene, (ItemContainerType)message.ContainerType);
            
            for (int i = 0; i < message.ItemInfoList.Count; i++)
            {
                Item item = ItemFactory.Create(scene, message.ItemInfoList[i]);
                ItemHelper.AddItem(scene, item, (ItemContainerType)message.ContainerType);
            }

            await ETTask.CompletedTask;
        }
    }
}