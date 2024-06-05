namespace ET.Server
{
    //登录中心服处理 record协议
    [MessageHandler(SceneType.LoginCenter)]
    public class G2L_AddLoginRecordHandler : MessageHandler<Scene, G2L_AddLoginRecord, L2G_AddLoginRecord>
    {
        protected override async ETTask Run(Scene scene, G2L_AddLoginRecord request, L2G_AddLoginRecord response)
        {
            //记录当前账户选择哪个区服进行连接  用户处理顶号操作
            scene.GetComponent<LoginInfoRecordComponent>().Remove(request.AccountName.GetLongHashCode());
            scene.GetComponent<LoginInfoRecordComponent>().Add(request.AccountName.GetLongHashCode(), request.ServerId);
            await ETTask.CompletedTask;
        }
    }
}