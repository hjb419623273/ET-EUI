namespace ET.Server
{
    [MessageHandler(SceneType.Gate)]
    [FriendOfAttribute(typeof(ET.Server.Player))]
    public class L2G_DisconnectGateUnitHandler : MessageHandler<Scene, L2G_DisconnectGateUnit, G2L_DisconnectGateUnit>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnit request, G2L_DisconnectGateUnit response)
        {
            CoroutineLockComponent coroutineLockComponent = scene.GetComponent<CoroutineLockComponent>();
            using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, request.AccountName.GetLongHashCode()))
            {
                PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
                Player player = playerComponent.GetByAccount(request.AccountName);

                //玩家还未登录进网关 未在线状态
                if (player == null)
                {
                    return;
                }

                scene.GetComponent<GateSessionKeyComponent>().Remove(request.AccountName.GetLongHashCode());
                
                Session gateSession = player.GetComponent<PlayerSessionComponent>()?.Session;
                if (gateSession != null && !gateSession.IsDisposed)
                {
                    //通知玩家断开连接  顶号操作
                    A2C_Disconnect a2CDisconnect = A2C_Disconnect.Create();
                    a2CDisconnect.Error = ErrorCode.ERR_OtherAccountLogin;
                    gateSession.Send(a2CDisconnect);
                    gateSession?.Disconnect().Coroutine();
                }

                if ( player.GetComponent<PlayerSessionComponent>()?.Session != null)
                {
                    player.GetComponent<PlayerSessionComponent>().Session = null;
                }
                player.AddComponent<PlayerOfflineOutTimeComponent>();
            }
        }
    }
}