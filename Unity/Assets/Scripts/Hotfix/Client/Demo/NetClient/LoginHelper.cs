namespace ET.Client
{
    [FriendOfAttribute(typeof(ET.ServerInfo))]
    [FriendOfAttribute(typeof(ET.Client.RecordAccountInfoComponect))]
    [FriendOfAttribute(typeof(ET.Client.ClientServerInfosComponent))]
    [FriendOfAttribute(typeof(ET.Client.RoleInfosComponent))]
    public static class LoginHelper
    {
        //账号登录验证
        public static async ETTask<int> Login(Scene scene, string account, string password)
        {
            //每次请求都会移除旧的ClientSenderComponent组件并且创建一个新的 (因为消息需要fiber发送的原因？)
            //和之前登录连接区别 所以删除后创建新的
            scene.RemoveComponent<ClientSenderComponent>();
            ClientSenderComponent clientSenderComponent = scene.AddComponent<ClientSenderComponent>();

            //向服务器发送登录消息 
            NetClient2Main_Login response = await clientSenderComponent.LoginAsync(account, password);
            if (response.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"请求登录失败，返回错误{response.Error}");
                return response.Error;
            }
            Log.Debug("请求登录成功！！！");
            RecordAccountInfoComponect recordAccountInfoComponect = scene.GetComponent<RecordAccountInfoComponect>();
            recordAccountInfoComponect.Account = account;
            recordAccountInfoComponect.Token = response.Token;

            return response.Error;
        }


        //请求服务器列表
        public static async ETTask<int> GetServerInfos(Scene scene)
        {
            ClientSenderComponent clientSenderComponent = scene.GetComponent<ClientSenderComponent>();

            RecordAccountInfoComponect recordAccountInfoComponect = scene.GetComponent<RecordAccountInfoComponect>();
            C2R_GetServerInfos c2RGetServerInfos = C2R_GetServerInfos.Create();
            c2RGetServerInfos.Account = recordAccountInfoComponect.Account;
            c2RGetServerInfos.Token = recordAccountInfoComponect.Token;
            R2C_GetServerInfos r2CGetServerInfos = await clientSenderComponent.Call(c2RGetServerInfos) as R2C_GetServerInfos; ;

            if (r2CGetServerInfos.Error != ErrorCode.ERR_Success)
            {
                Log.Error("请求服务器列表失败！");
                return r2CGetServerInfos.Error;
            }

            foreach (var serverInfoProto in r2CGetServerInfos.ServerInfosList)
            {
                ServerInfo serverInfo = scene.GetComponent<ClientServerInfosComponent>().AddChildWithId<ServerInfo>(serverInfoProto.Id);
                serverInfo.FromMessage(serverInfoProto);
                scene.GetComponent<ClientServerInfosComponent>().Add(serverInfo);
            }

            return ErrorCode.ERR_Success;
        }

        //获取角色信息
        public static async ETTask<int> GetRoles(Scene scene)
        {
            ClientSenderComponent clientSenderComponent = scene.GetComponent<ClientSenderComponent>();

            RecordAccountInfoComponect recordAccountInfoComponect = scene.GetComponent<RecordAccountInfoComponect>();
            ClientServerInfosComponent clientServerInfosComponent = scene.GetComponent<ClientServerInfosComponent>();

            C2R_GetRoles c2RGetRoles = C2R_GetRoles.Create();
            c2RGetRoles.Token = recordAccountInfoComponect.Token;
            c2RGetRoles.Account = recordAccountInfoComponect.Account;
            c2RGetRoles.ServerId = clientServerInfosComponent.CurrentServerId;
            R2C_GetRoles r2CGetRoles = await clientSenderComponent.Call(c2RGetRoles) as R2C_GetRoles;
            if (r2CGetRoles.Error != ErrorCode.ERR_Success)
            {
                Log.Error("请求区服角色列表失败！");
                return r2CGetRoles.Error;
            }
            foreach (var roleInfoProto in r2CGetRoles.RoleInfo)
            {
                RoleInfo roleInfo = scene.GetComponent<RoleInfosComponent>().AddChildWithId<RoleInfo>(roleInfoProto.Id);
                roleInfo.FromMessage(roleInfoProto);
                scene.GetComponent<RoleInfosComponent>().Add(roleInfo);
            }

            return ErrorCode.ERR_Success;
        }

        //创建新角色
        public static async ETTask<int> CreateRole(Scene scene, string roleName)
        {
            ClientSenderComponent clientSenderComponent = scene.GetComponent<ClientSenderComponent>();

            RecordAccountInfoComponect recordAccountInfoComponect = scene.GetComponent<RecordAccountInfoComponect>();
            ClientServerInfosComponent clientServerInfosComponent = scene.GetComponent<ClientServerInfosComponent>();

            C2R_CreateRole c2RCreateRole = C2R_CreateRole.Create();
            c2RCreateRole.Token = recordAccountInfoComponect.Token;
            c2RCreateRole.Account = recordAccountInfoComponect.Account;
            c2RCreateRole.ServerId = clientServerInfosComponent.CurrentServerId;
            c2RCreateRole.Name = roleName;

            R2C_CreateRole r2CCreateRole = await clientSenderComponent.Call(c2RCreateRole) as R2C_CreateRole;

            if (r2CCreateRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error("创建区服角色失败！");
                return r2CCreateRole.Error;
            }

            //新角色信息放入component
            RoleInfo roleInfo = scene.GetComponent<RoleInfosComponent>().AddChildWithId<RoleInfo>(r2CCreateRole.RoleInfo.Id);
            roleInfo.FromMessage(r2CCreateRole.RoleInfo);
            scene.GetComponent<RoleInfosComponent>().Add(roleInfo);
            return r2CCreateRole.Error;
        }

        //删除角色
        public static async ETTask<int> DeleteRole(Scene scene)
        {
            ClientSenderComponent clientSenderComponent = scene.GetComponent<ClientSenderComponent>();

            RecordAccountInfoComponect recordAccountInfoComponect = scene.GetComponent<RecordAccountInfoComponect>();
            ClientServerInfosComponent clientServerInfosComponent = scene.GetComponent<ClientServerInfosComponent>();
            RoleInfosComponent roleInfosComponent = scene.GetComponent<RoleInfosComponent>();

            C2R_DeleteRole c2RDeleteRole = C2R_DeleteRole.Create();
            c2RDeleteRole.Token = recordAccountInfoComponect.Token;
            c2RDeleteRole.Account = recordAccountInfoComponect.Account;
            c2RDeleteRole.RoleInfoId = roleInfosComponent.CurrentRoleId;
            c2RDeleteRole.ServerId = clientServerInfosComponent.CurrentServerId; 
            R2C_DeleteRole r2CDeleteRole = await clientSenderComponent.Call(c2RDeleteRole) as R2C_DeleteRole;
            
            if (r2CDeleteRole.Error != ErrorCode.ERR_Success)
            {
                Log.Error("删除角色失败！");
                return r2CDeleteRole.Error;
            }

            int roleIndex = roleInfosComponent.RoleInfos.FindIndex((refinfo) =>
            {
                RoleInfo info = refinfo;
                return info.Id == r2CDeleteRole.DeletedRoleInfoId;
            });
            
            roleInfosComponent.RoleInfos.RemoveAt(roleIndex);
            return ErrorCode.ERR_Success;
        }

        //获取realm令牌
        public static async ETTask<int> GetRealmKey(Scene scene)
        {
            ClientSenderComponent clientSenderComponent = scene.GetComponent<ClientSenderComponent>();

            RecordAccountInfoComponect recordAccountInfoComponect = scene.GetComponent<RecordAccountInfoComponect>();
            ClientServerInfosComponent clientServerInfosComponent = scene.GetComponent<ClientServerInfosComponent>();
            
            //请求获取RealmKey
            C2R_GetRealmKey c2RGetRealmKey = C2R_GetRealmKey.Create();
            c2RGetRealmKey.Token = recordAccountInfoComponect.Token;
            c2RGetRealmKey.Account = recordAccountInfoComponect.Account;
            c2RGetRealmKey.ServerId = clientServerInfosComponent.CurrentServerId;
            
            R2C_GetRealmKey r2CGetRealmKey =  await clientSenderComponent.Call(c2RGetRealmKey) as R2C_GetRealmKey;
            
            if (r2CGetRealmKey.Error != ErrorCode.ERR_Success)
            {
                Log.Error("获取RealmKey失败！");
                return r2CGetRealmKey.Error;
            }

            recordAccountInfoComponect.RealmAddress = r2CGetRealmKey.Address;
            recordAccountInfoComponect.RealmKey = r2CGetRealmKey.Key;
            recordAccountInfoComponect.GateId = r2CGetRealmKey.GateId;

            return ErrorCode.ERR_Success;
        }
        
        //连接Gate并进去Game
        public static async ETTask<int> EnterGame(Scene scene)
        {
            ClientSenderComponent clientSenderComponent = scene.GetComponent<ClientSenderComponent>();

            RecordAccountInfoComponect recordAccountInfoComponect = scene.GetComponent<RecordAccountInfoComponect>();
            RoleInfosComponent roleInfosComponent = scene.GetComponent<RoleInfosComponent>();
            PlayerComponent playerComponent = scene.GetComponent<PlayerComponent>();
            
            //请求游戏角色传送入Game
            NetClient2Main_LoginGame netClient2MainLoginGame = await clientSenderComponent.LoginGameAsync(recordAccountInfoComponect.Account, recordAccountInfoComponect.RealmKey,roleInfosComponent.CurrentRoleId,recordAccountInfoComponect.RealmAddress);
            
            if (netClient2MainLoginGame.Error != ErrorCode.ERR_Success)
            {
                Log.Error($"进入游戏失败：{netClient2MainLoginGame.Error}");
                return netClient2MainLoginGame.Error;
            }
            
            Log.Debug("进入游戏成功！！！");
            playerComponent.MyId = netClient2MainLoginGame.PlayerId;
     
            //await EventSystem.Instance.PublishAsync(scene, new LoginFinish());
            // Wait_SceneChangeFinish
            await scene.Root().GetComponent<ObjectWait>().Wait<Wait_SceneChangeFinish>();
            return ErrorCode.ERR_Success;
        }
        
        
        // public static async ETTask Login(Scene root, string account, string password)
        // {
        //     root.RemoveComponent<ClientSenderComponent>();
        //     
        //     ClientSenderComponent clientSenderComponent = root.AddComponent<ClientSenderComponent>();
        //     
        //     var response = await clientSenderComponent.LoginAsync(account, password);
        //     if (response.Error != ErrorCode.ERR_Success)
        //     {
        //         Log.Error($"请求登录失败，返回错误{response.Error}");
        //         return ;
        //     }
        //     Log.Debug("请求登录成功！！！");
        //     string Token = response.Token;
        //     
        //     //root.GetComponent<PlayerComponent>().MyId = response.PlayerId;
        //     
        //     //获取服务器列表
        //     C2R_GetServerInfos c2RGetServerInfos = C2R_GetServerInfos.Create();
        //     c2RGetServerInfos.Account = account;
        //     c2RGetServerInfos.Token   = response.Token;
        //     R2C_GetServerInfos r2CGetServerInfos =  await clientSenderComponent.Call(c2RGetServerInfos) as R2C_GetServerInfos;
        //     if (r2CGetServerInfos.Error != ErrorCode.ERR_Success)
        //     {
        //         Log.Error("请求服务器列表失败！");
        //         return;
        //     }
        //
        //     ServerInfoProto serverInfoProto = r2CGetServerInfos.ServerInfosList[0];
        //     Log.Debug($"请求服务器列表成功, 区服名称:{serverInfoProto.ServerName} 区服ID:{serverInfoProto.Id}");
        //     
        //     //获取区服角色列表
        //     C2R_GetRoles c2RGetRoles = C2R_GetRoles.Create();
        //     c2RGetRoles.Token        = Token;
        //     c2RGetRoles.Account      = account;
        //     c2RGetRoles.ServerId     = serverInfoProto.Id;
        //     R2C_GetRoles r2CGetRoles =  await clientSenderComponent.Call(c2RGetRoles) as R2C_GetRoles;
        //     if (r2CGetRoles.Error != ErrorCode.ERR_Success)
        //     {
        //         Log.Error("请求区服角色列表失败！");
        //         return;
        //     }
        //
        //     RoleInfoProto roleInfoProto = default;
        //     if (r2CGetRoles.RoleInfo.Count <= 0)
        //     {
        //         //无角色信息 则创建角色信息
        //         C2R_CreateRole c2RCreateRole = C2R_CreateRole.Create();
        //         c2RCreateRole.Token        = Token;
        //         c2RCreateRole.Account      = account;
        //         c2RCreateRole.ServerId     = serverInfoProto.Id;
        //         c2RCreateRole.Name         = account;
        //         
        //         R2C_CreateRole r2CCreateRole =  await clientSenderComponent.Call(c2RCreateRole) as R2C_CreateRole;
        //
        //         if (r2CCreateRole.Error != ErrorCode.ERR_Success)
        //         {
        //             Log.Error("创建区服角色失败！");
        //             return;
        //         }
        //
        //         roleInfoProto = r2CCreateRole.RoleInfo;
        //     }
        //     else
        //     {
        //         roleInfoProto = r2CGetRoles.RoleInfo[0];
        //     }
        //     
        //     //请求获取RealmKey
        //     C2R_GetRealmKey c2RGetRealmKey = C2R_GetRealmKey.Create();
        //     c2RGetRealmKey.Token = Token;
        //     c2RGetRealmKey.Account = account;
        //     c2RGetRealmKey.ServerId = serverInfoProto.Id;
        //     R2C_GetRealmKey r2CGetRealmKey =  await clientSenderComponent.Call(c2RGetRealmKey) as R2C_GetRealmKey;
        //
        //     if (r2CGetRealmKey.Error != ErrorCode.ERR_Success)
        //     {
        //         Log.Error("获取RealmKey失败！");
        //         return;
        //     }
        //
        //     
        //     //请求游戏角色进入Map地图
        //     NetClient2Main_LoginGame netClient2MainLoginGame = await clientSenderComponent.LoginGameAsync(account, r2CGetRealmKey.Key,roleInfoProto.Id,r2CGetRealmKey.Address);
        //     if (netClient2MainLoginGame.Error != ErrorCode.ERR_Success)
        //     {
        //         Log.Error($"进入游戏失败：{netClient2MainLoginGame.Error}");
        //         return;
        //     }
        //     
        //     Log.Debug("进入游戏成功！！！");
        //     
        //     await EventSystem.Instance.PublishAsync(root, new LoginFinish());
        // }
    }
}