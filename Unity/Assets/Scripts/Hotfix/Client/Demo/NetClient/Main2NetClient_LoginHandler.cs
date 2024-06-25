using System;
using System.Net;
using System.Net.Sockets;

namespace ET.Client
{
    [MessageHandler(SceneType.NetClient)]
    public class Main2NetClient_LoginHandler: MessageHandler<Scene, Main2NetClient_Login, NetClient2Main_Login>
    {
        protected override async ETTask Run(Scene root, Main2NetClient_Login request, NetClient2Main_Login response)
        {
            string account  = request.Account;
            string password = request.Password;
            // 创建一个ETModel层的Session
            root.RemoveComponent<RouterAddressComponent>();
            
            // 获取路由跟realmDispatcher地址
            //使用配置好的ip和地址创建RouterAddressComponent (上线后取云服务端口和ip)
            RouterAddressComponent routerAddressComponent =
                    root.AddComponent<RouterAddressComponent, string, int>(ConstValue.RouterHttpHost, ConstValue.RouterHttpPort);
            
            //获取Router节点服务器List并且存储在RouterAddressComponent中
            //同时会获取Realm网关负载均衡服务器地址 (在正式服中会配置多个 Realm网关负载均衡服务器分摊登录压力)
            await routerAddressComponent.Init();
            
            //添加客户端和服务器通讯组件 NetComponent
            root.AddComponent<NetComponent, AddressFamily, NetworkProtocol>(routerAddressComponent.RouterManagerIPAddress.AddressFamily, NetworkProtocol.UDP);
            root.GetComponent<FiberParentComponent>().ParentFiberId = request.OwnerFiberId;

            NetComponent netComponent = root.GetComponent<NetComponent>();
            
            //对游戏长好进行取模操作来获取合适的Realm网关负载均衡服务器地址
            IPEndPoint realmAddress = routerAddressComponent.GetRealmAddress(account);

            R2C_LoginAccount r2CLogin;
            
            //建立与Realm网关负载均衡服务器Session(类似电话？) 注意与客户端通讯还是需要Router转发
            Session session = await netComponent.CreateRouterSession(realmAddress, account, password);
            
            //C  == Client  R == Router 
            C2R_LoginAccount c2RLogin = C2R_LoginAccount.Create();
            c2RLogin.AccountName = account;
            c2RLogin.Password    = password;
            r2CLogin = (R2C_LoginAccount)await session.Call(c2RLogin);
            
            if (r2CLogin.Error == ErrorCode.ERR_Success)
            {
                root.AddComponent<SessionComponent>().Session = session;
            }
            else
            {
                session?.Dispose();
            }
            response.Token = r2CLogin.Token;
            response.Error = r2CLogin.Error;
        }
    }
}