using System;

namespace ET.Server
{
    //从gate网关传送至游戏服（map）
    [MessageSessionHandler(SceneType.Gate)]
    public class C2G_EnterGameHanlder : MessageSessionHandler<C2G_EnterGame, G2C_EnterGame>
    {
        protected override async ETTask Run(Session session, C2G_EnterGame request, G2C_EnterGame response)
        {
            //重复请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                return;
            }
                
            //登录进gate网关的时候会给seesion增加一个SessionPlayerComponent组件 
            //当前客户端并有有经过gate网关 
            SessionPlayerComponent sessionPlayerComponent = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerComponent == null)
            {
                response.Error = ErrorCode.ERR_SessionPlayerError;
                return;
            }

            Player player = sessionPlayerComponent.Player;
            
            if (player != null || player.IsDisposed)
            {
                response.Error = ErrorCode.ERR_NonePlayerError;
                return;
            }

            CoroutineLockComponent coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();

            long instanceId = session.InstanceId;

            // CoroutineLockType.LoginGate 在任意时刻只有一个数据在操作 可以查看该协程锁用的地方
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, player.Account.GetLongHashCode()))
                {
                    // 异步逻辑等待时 session'可能被释放 或者play被释放
                    if (instanceId != session.InstanceId || player.IsDisposed)
                    {
                        response.Error = ErrorCode.ERR_PlayerSessionError;
                        return;
                    }
                    
                    //在map中
                    if (player.PlayerState == PlayerState.Game)
                    {
                        try
                        {
                            G2M_SecondLogin g2MSecondLogin = G2M_SecondLogin.Create();
                            IResponse reqEnter = await session.Root().GetComponent<MessageLocationSenderComponent>()
                                    .Get(LocationType.Unit).Call(player.UnitId, g2MSecondLogin);
                            if (reqEnter.Error == ErrorCode.ERR_Success)
                            {
                                Log.Console("作业:二次登陆逻辑，补全下发切换场景消息");
                                return;
                            }
                            Log.Error("二次登录失败  "+ reqEnter.Error +" | " + reqEnter.Message);
                            response.Error = ErrorCode.ERR_ReEnterGameError;
                            await DisconnectHelper.KickPlayerNoLock(player);
                            session.Disconnect().Coroutine();
                        }
                        catch (Exception e)
                        {
                            Log.Error("二次登录失败  " + e);
                            response.Error = ErrorCode.ERR_ReEnterGameError2;
                            await DisconnectHelper.KickPlayerNoLock(player);
                            session.Disconnect().Coroutine();
                        }
                    }

                    try
                    {
                        // 在Gate上动态创建一个Map Scene， 把unit从DB中加载放进来，然后传送到真正的map中 这样登录和传送的逻辑就完全一致了
                        GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
                        gateMapComponent.Scene = await GateMapFactory.Create(gateMapComponent, player.Id, IdGenerater.Instance.GenerateInstanceId(), "GateMap");
                        Scene scene = gateMapComponent.Scene;
                        
                        //这里可以从DB中加载Unit
                        //这里正确流程是从数据库读取玩家数据 假如第一次登录创建数据存入数据库中
                        Unit unit = UnitFactory.Create(scene, player.Id, UnitType.Player);
                        long unitId = unit.Id;

                        StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.Zone(), "Map1");
                        
                        //等待一帧的最后再传送 先让G2C_EnterMap返回， 否则消息传递过去比G2C_EnterMap还早
                        TransferHelper.TransferAtFrameFinish(unit, startSceneConfig.ActorId, startSceneConfig.Name).Coroutine();
                        player.UnitId = unitId;
                        response.MyUnitId = unitId;
                        player.PlayerState = PlayerState.Game;
                    }
                    catch (Exception e)
                    {
                        Log.Error($"角色进入游戏逻辑服出现问题 账号id：{player.Account} 角色id:{player.Id} 异常信息: {e}");
                        response.Error = ErrorCode.ERR_EnterGameError;
                        await DisconnectHelper.KickPlayerNoLock(player);
                        session.Disconnect().Coroutine();
                    }
                    
                }
            }
        }
    }  
}
