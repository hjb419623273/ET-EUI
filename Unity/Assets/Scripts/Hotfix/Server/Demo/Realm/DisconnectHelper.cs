using System.Threading;

namespace ET.Server
{
    //1秒后删除
    public static class DisconnectHelper
    {
        //Session类的扩展函数
        public static async ETTask Disconnect(this Session self)
        {
            //是否为空 或者被释放
            if (self == null || self.IsDisposed)
            {
                return;
            }

            long instancedId = self.InstanceId;

            TimerComponent timerComponent = self.Root().GetComponent<TimerComponent>();
            await timerComponent.WaitAsync(1000);
            
            //防止等待过程中在外界被删除或者被重复利用 instanceid会变化
            if (self.InstanceId != instancedId)
            {
                return;
            }
            self.Dispose();
        }

        public static async ETTask KickPlayerNoLock(Player player)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }

            switch (player.PlayerState)
            {
                case PlayerState.Disconnect:
                    break;
                case PlayerState.Gate:
                    break;
                case PlayerState.Game:
                    //通知游戏逻辑服（map）下线Unit角色逻辑 并将数据存入数据库
                    //player.Root() Gate网关实体 
                    //GetComponent<MessageLocationSenderComponent>().Get(LocationType.Unit) 找到对应unit在那个map服务器上
                    //player.UnitIdu游戏角色id
                    var m2GRequestExitGame = (M2G_RequestExitGame)await player.Root().GetComponent<MessageLocationSenderComponent>()
                            .Get(LocationType.Unit).Call(player.UnitId, G2M_RequestExitGame.Create());
                    
                    //通知移除账号角色登录信息
                    G2L_RemoveLoginRecord g2LRemoveLoginRecord = G2L_RemoveLoginRecord.Create();
                    g2LRemoveLoginRecord.AccountName = player.Account;
                    g2LRemoveLoginRecord.ServerId = player.Zone();
                    
                    //发送给登录中心服
                    var L2G_RemoveLoginRecord = (L2G_RemoveLoginRecord) await player.Root().GetComponent<MessageSender>()
                            .Call(StartSceneConfigCategory.Instance.LoginCenterConfig.ActorId, g2LRemoveLoginRecord);
                    break;
            }

            TimerComponent timerComponent = player.Root().GetComponent<TimerComponent>();
            player.PlayerState = PlayerState.Disconnect;
            await player.GetComponent<PlayerSessionComponent>().RemoveLocation(LocationType.GateSession);
            await player.RemoveLocation(LocationType.Player);
            player.Root().GetComponent<PlayerComponent>()?.Remove(player);
            player?.Dispose();
            await timerComponent.WaitAsync(300);
        }

        public static async ETTask KickPlayer(Player player)
        {
            if (player == null || player.IsDisposed)
            {
                return;
            }

            long instanceId = player.InstanceId;
            
            CoroutineLockComponent coroutineLockComponent = player.Root().GetComponent<CoroutineLockComponent>();

            using (await coroutineLockComponent.Wait(CoroutineLockType.LoginGate, player.Account.GetLongHashCode()))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                {
                    return;
                }

                await KickPlayerNoLock(player);
            }
        }
    }
}