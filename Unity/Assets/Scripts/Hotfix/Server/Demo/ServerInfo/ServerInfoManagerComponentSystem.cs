namespace ET.Server
{
    [EntitySystemOf(typeof(ServerInfoManagerComponent))]
    [FriendOfAttribute(typeof(ET.Server.ServerInfoManagerComponent))]
    [FriendOfAttribute(typeof(ET.ServerInfo))]
    public static partial class ServerInfoManagerComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.ServerInfoManagerComponent self)
        {
            self.Load();
        }
        
        [EntitySystem]
        private static void Destroy(this ET.Server.ServerInfoManagerComponent self)
        {
            foreach (var serverInfoRef in self.ServerInfos)
            {
                ServerInfo serverInfo = serverInfoRef;
                serverInfo?.Dispose();
            }
            self.ServerInfos.Clear();
        }
        
        public static void Load(this ServerInfoManagerComponent self)
        {
            foreach (var serverInfoRef in self.ServerInfos)
            {
                ServerInfo serverInfo = serverInfoRef;
                serverInfo?.Dispose();
            }
            self.ServerInfos.Clear();

            var serverInfoConfigs = StartZoneConfigCategory.Instance.GetAll();

            foreach (var info in serverInfoConfigs.Values)
            {
                if (info.ZoneType != 1)
                {
                    continue;
                }
                
                ServerInfo newServerInfo = self.AddChildWithId<ServerInfo>(info.Id);
                newServerInfo.ServerName = info.DBName;
                newServerInfo.Status     = (int)ServerStatus.Normal;
                self.ServerInfos.Add(newServerInfo);
            }
        }

    }
}