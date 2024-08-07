namespace ET.Client
{
    [EntitySystemOf(typeof(RoleInfosComponent))]
    [FriendOfAttribute(typeof(ET.Client.RoleInfosComponent))]
    public static partial class RoleInfosComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.RoleInfosComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.RoleInfosComponent self)
        {
            foreach (RoleInfo roleInfo in self.RoleInfos)
            {
                roleInfo?.Dispose();
            }
            self.RoleInfos.Clear();
            self.CurrentRoleId = 0;
        }

        public static void Add(this RoleInfosComponent self, RoleInfo serverInfo)
        {
            self.RoleInfos.Add(serverInfo);
        }
        
    }
}
