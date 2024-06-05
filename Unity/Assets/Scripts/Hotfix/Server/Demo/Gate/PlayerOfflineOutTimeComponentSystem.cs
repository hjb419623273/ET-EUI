namespace ET.Server
{
    
    //申明一個定時器
    [Invoke(TimerInvokeType.PlayerOfflineOutTime)]
    public class PlayerOfflineOutTime : ATimer<PlayerOfflineOutTimeComponent>
    {
        protected override void Run(PlayerOfflineOutTimeComponent t)
        {
            t?.kickPlayer();
        }
    }

    [EntitySystemOf(typeof(PlayerOfflineOutTimeComponent))]
    [FriendOfAttribute(typeof(ET.Server.PlayerOfflineOutTimeComponent))]
    public static partial class PlayerOfflineOutTimeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.PlayerOfflineOutTimeComponent self)
        {
            self.Timer = self.Root().GetComponent<TimerComponent>()
                    .NewOnceTimer(TimeInfo.Instance.ServerNow() * 10000, TimerInvokeType.PlayerOfflineOutTime, self);
            
        }
        
        [EntitySystem]
        private static void Destroy(this ET.Server.PlayerOfflineOutTimeComponent self)
        {
            //移除定时器
            self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
        }
        
        public static void kickPlayer(this PlayerOfflineOutTimeComponent self)
        {
            DisconnectHelper.KickPlayer(self.GetParent<Player>()).Coroutine();
        }
    }
}