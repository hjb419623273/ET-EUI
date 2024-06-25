namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    public class M2C_NoticeUnitNumericHandler : MessageHandler<Scene, M2C_NoticeUnitNumeric>
    {
        protected override async ETTask Run(Scene scene, M2C_NoticeUnitNumeric message)
        {
            Log.Warning(">>>>>>>client NumericType:"+message.NumericType + " value:"+message.NewValue);
            scene.CurrentScene()?.GetComponent<UnitComponent>()?
                    .Get(message.UnitId)?.GetComponent<NumericComponent>()?.Set(message.NumericType, message.NewValue);
            
            await ETTask.CompletedTask;
        }
    }
}