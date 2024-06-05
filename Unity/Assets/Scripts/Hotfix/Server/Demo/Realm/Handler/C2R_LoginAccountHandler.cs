using System.Text.RegularExpressions;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]    //realm服务器处理这个 session
    [FriendOfAttribute(typeof(ET.Server.Account))]
    public class C2R_LoginAccountHandler : MessageSessionHandler<C2R_LoginAccount, R2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2R_LoginAccount request, R2C_LoginAccount response)
        {
            session.RemoveComponent<SessionAcceptTimeoutComponent>();   //移除Time组件 因为session被创建出来时 5秒会删除该session

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session.Disconnect().Coroutine();       //先回复给client 1秒后删除session
                return;
            }

            if (string.IsNullOrEmpty(request.AccountName) || string.IsNullOrEmpty(request.Password))
            {
                response.Error = ErrorCode.ERR_LoginInfoIsNull;
                session.Disconnect().Coroutine();
                return;
            }

            if (!Regex.IsMatch(request.AccountName.Trim(), @"^(?=.*[0-9],*)(?=.*[A-Z].*)(?=.*[a-z].*).{6, 15}$"))
            {
                response.Error = ErrorCode.ERR_AccountNameFormError;
                session.Disconnect().Coroutine();
                return;
            }

            if (!Regex.IsMatch(request.Password.Trim(), @"^[A-Za-z0-9]+$"))
            {
                response.Error = ErrorCode.ERR_PasswordFormError;
                session.Disconnect().Coroutine();
                return;
            }

            //添加一个携程锁 防止多次调用
            CoroutineLockComponent coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();
            //using的用法 using结束时Dispose方法会被自动调用
            using (session.AddComponent<SessionLockingComponent>())
            {
                //添加一个携程锁组件 防止同一个账号Name重复注册
                using (await coroutineLockComponent.Wait(CoroutineLockType.LoginAccount, request.AccountName.GetHashCode()))
                {
                    //session.Root Realm
                    //session.Zone() StartSceneConfig中配置的zone 1000
                    DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());

                    var accountInfoList = await dbComponent.Query<Account>(d => d.AccountName.Equals(request.AccountName));

                    Account account = null;

                    if (accountInfoList != null && accountInfoList.Count > 0)
                    {
                        account = accountInfoList[0];
                        session.AddChild(account);
                        //黑名单
                        if (account.AccountType == (int)AccountType.BlackList)
                        {
                            response.Error = ErrorCode.ERR_AccountInBlackListErroer;
                            session.Disconnect().Coroutine();
                            account?.Dispose(); //这里可以不手动释放因为 account 作为一个子组件加入到session中
                            return;
                        }
                        
                        //密码错误
                        if (!account.Password.Equals(request.Password))
                        {
                            response.Error = ErrorCode.ERR_LoginPasswordError;
                            session.Disconnect().Coroutine();
                            account?.Dispose();
                            return;
                        }
                    }
                    else
                    {
                        account = session.AddChild<Account>();
                        //Trim方法返回一个新字符串，它从当前字符串中删除了一组指定字符的所有前导匹配项和尾随匹配项
                        //删除空格
                        account.AccountName = request.AccountName.Trim();
                        account.Password = request.Password;
                        //服务器时间
                        account.CreateTime = TimeInfo.Instance.ServerNow();
                        account.AccountType = (int)AccountType.General;
                        await dbComponent.Save<Account>(account);
                    }
                    
                    R2L_LoginAccountRequest r2LLoginAccountRequest = R2L_LoginAccountRequest.Create();
                    r2LLoginAccountRequest.AccountName = request.AccountName;

                    StartSceneConfig loginCenterConfig = StartSceneConfigCategory.Instance.LoginCenterConfig;
                    //像中心服务器发送 R2L_LoginAccountRequest消息 中心服回复L2R_LoginAccountRequest
                    var loginAccountResponse =
                            await session.Fiber().Root.GetComponent<MessageSender>().Call(loginCenterConfig.ActorId, r2LLoginAccountRequest) as
                                    L2R_LoginAccountRequest;
                    if (loginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        response.Error = loginAccountResponse.Error;
                        session?.Disconnect().Coroutine();
                        account?.Dispose();
                        return;
                    }
                    
                    // otherSession其他设备登入该账号并且还在保持连接
                    // e.g A玩家先登入 B玩家后登入 通知玩家A账号被登陆 所以这里需要拿到先登录玩家的Session 发送短发连接消息
                    Session otherSession = session.Root().GetComponent<AccountSessionsComponent>().Get(request.AccountName);
                    
                    otherSession?.Send(A2C_Disconnect.Create());
                    otherSession?.Disconnect().Coroutine();
                    session.Root().GetComponent<AccountSessionsComponent>().Add(request.AccountName, session);
                    session.AddComponent<AccountCheckOutTimeComponent, string>(request.AccountName);

                    //session放入AccountComponent来管理
                    string Token = TimeInfo.Instance.ServerNow().ToString() + RandomGenerator.RandomNumber(int.MinValue, int.MaxValue).ToString();
                    session.Root().GetComponent<TokenComponent>().Remove(request.AccountName);
                    session.Root().GetComponent<TokenComponent>().Add(request.AccountName, Token);

                    response.Token = Token;
                    
                    account?.Dispose();
                }
            }
        }
    }
}