using System;
using System.Collections.Generic;
using System.Net;


namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOfAttribute(typeof(ET.Server.AccountInfo))]
    public class C2R_LoginHandler : MessageSessionHandler<C2R_Login, R2C_Login>
    {
        protected override async ETTask Run(Session session, C2R_Login request, R2C_Login response)
        {
            //账号密码为空 删除session连接
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoIsNull;
                CloseSession(session).Coroutine();
                return;
            }
            
            //添加携程锁 预防一个账户多方登录
            using (await  session.Root().GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.LoginAccount, request.Account.GetLongHashCode()))
            {
                DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                List<AccountInfo> accountInfos = await dbComponent.Query<AccountInfo>(accountInfo => accountInfo.Account == request.Account);

                if (accountInfos.Count <= 0)
                {
                    AccountInfosComponent accountInfosComponent =
                            session.GetComponent<AccountInfosComponent>() ?? session.AddComponent<AccountInfosComponent>();
                    AccountInfo accountInfo =  accountInfosComponent.AddChild<AccountInfo>();
                    accountInfo.Account = request.Account;
                    accountInfo.Password = request.Password;
                    await dbComponent.Save(accountInfo);            //保存到数据库
                }
                else
                {
                    AccountInfo accountInfo = accountInfos[0];
                    if (accountInfo.Password != request.Password)
                    {
                        response.Error = ErrorCode.ERR_PasswordError;
                        CloseSession(session).Coroutine();
                        return;
                    }
                }
            }
       
            
            // 随机分配一个Gate
            StartSceneConfig config = RealmGateAddressHelper.GetGate(session.Zone(), request.Account);
            Log.Debug($"gate address: {config}");

            // 向gate请求一个key,客户端可以拿着这个key连接gate
            R2G_GetLoginKey r2GGetLoginKey = R2G_GetLoginKey.Create();
            r2GGetLoginKey.Account = request.Account;
            G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await session.Fiber().Root.GetComponent<MessageSender>().Call(
                config.ActorId, r2GGetLoginKey);

            response.Address = config.InnerIPPort.ToString();
            response.Key = g2RGetLoginKey.Key;
            response.GateId = g2RGetLoginKey.GateId;

            CloseSession(session).Coroutine();
        }

        private async ETTask CloseSession(Session session)
        {
            await session.Root().GetComponent<TimerComponent>().WaitAsync(1000);
            session.Dispose();
        }
    }
}
