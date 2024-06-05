using ET.Client;

namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    public class L2G_DisconnectGateUnitHandler : MessageHandler<Scene, L2G_DisconnectGateUnit, G2L_DisconnectGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnit request, G2L_DisconnectGateUnit response)
        {
            CoroutineLockComponent coroutineLockComponent = scene.GetComponent<CoroutineLockComponent>();
            using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, request.AccountName.GetLongHashCode()))
            {
                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                Player player = playerComponent.GetByAccount(request.AccountName);

                if (player == null)
                {
                    //玩家未登录到网关
                    return;
                }
                
                //执行踢人下线
                //从gate网关连接令牌key的组件中 移除连接令牌
                scene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountName.GetLongHashCode());

                //拿到玩家连接session
                Session gateSession = player.GetComponent<PlayerSessionComponent>()?.Session;
                
                //断线操作
                if (gateSession != null && !gateSession.IsDisposed)
                {
                    //发送消息给提出的客户端 附带错误码
                    A2C_Disconnect a2CDisconnect = A2C_Disconnect.Create();
                    a2CDisconnect.Error = ErrorCode.ERR_OtherAccountLogin;      
                    gateSession.Send(a2CDisconnect);
                    gateSession?.Disconnect().Coroutine();
                    
                }
                
                //置空 PlayerSessionComponent
                if (player.GetComponent<PlayerSessionComponent>()?.Session != null)
                {
                    player.GetComponent<PlayerSessionComponent>().Session = null;
                }
                
                //添加下线组件 触发定时器
                player.AddComponent<PlayerOfflineOutTimeComponent>();
            }
        }
    }
}