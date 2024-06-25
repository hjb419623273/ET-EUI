namespace ET.Client
{
    [EntitySystemOf(typeof(ClientServerInfosComponent))]
    [FriendOfAttribute(typeof(ET.Client.ClientServerInfosComponent))]
    public static partial class ClientServerInfosComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.ClientServerInfosComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.ClientServerInfosComponent self)
        {

        }

        public static void Add(this ClientServerInfosComponent self, ServerInfo serverInfo)
        {
            self.ServerInfoList.Add(serverInfo);
        }

        public static void ClearServerList(this ClientServerInfosComponent self)
        {
            foreach (ServerInfo serverInfo in self.ServerInfoList)
            {
                serverInfo?.Dispose();
            }
            
            self.ServerInfoList.Clear();
            self.CurrentServerId = 0;
        }
    }
}
