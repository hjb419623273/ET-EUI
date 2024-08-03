namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_AllProductionListHandler : MessageHandler<Scene, M2C_AllProductionList>
    {
        protected override async ETTask Run(Scene scene, M2C_AllProductionList message)
        {
            if (message.ProductionProtoList != null)
            {
                for (int i = 0; i < message.ProductionProtoList.Count; i++)
                {
                    scene.GetComponent<ForgeComponent>().AddOrUpdateProductionQueue(message.ProductionProtoList[i]);
                }
            }

            await ETTask.CompletedTask;
        }
    }
}