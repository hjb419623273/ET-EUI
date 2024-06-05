using System;
using System.Threading;

namespace ET.Server
{
    // Invoke 在拥有此控件的基础窗口句柄的现呈上同步执行指定的委托（同步）
    [Invoke(TimerInvokeType.AccountSessionCheckoutTime)]
    public class AccountSessionCheckoutTimer : ATimer<AccountCheckOutTimeComponent>
    {
        protected override void Run(AccountCheckOutTimeComponent t)
        {
            
        }
    }

    [EntitySystemOf(typeof(AccountCheckOutTimeComponent))]
    [FriendOfAttribute(typeof(ET.Server.AccountCheckOutTimeComponent))]    //https://www.yuque.com/u28961999/yms0nt/awksvs
    public static partial class AccountCheckOutTimeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.AccountCheckOutTimeComponent self, string accout)
        {
            self.Account = accout;
            self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
            //一次性定时器任务 十分钟后定时器任务被执行 AccountSessionCheckoutTimer
            self.Timer = self.Root().GetComponent<TimerComponent>()
                    .NewOnceTimer(TimeInfo.Instance.ServerNow() + 600000, TimerInvokeType.AccountSessionCheckoutTime, self);
        }

        [EntitySystem]
        private static void Destroy(this ET.Server.AccountCheckOutTimeComponent self)
        {
            //ref 引用
            self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
        }

        public static void DeleteSession(this AccountCheckOutTimeComponent self)
        {
            Session session = self.GetParent<Session>();

            Session originSession = session.Root().GetComponent<AccountSessionsComponent>().Get(self.Account);
            if (originSession != null && session.InstanceId == originSession.InstanceId)
            {
                session.Root().GetComponent<AccountSessionsComponent>().Remove(self.Account);
            }

            //通知客户端断开连接
            A2C_Disconnect a2CDisconnect = A2C_Disconnect.Create();
            a2CDisconnect.Error = 1;
            session?.Send(a2CDisconnect);
            session?.Disconnect().Coroutine();
        }
    }
}