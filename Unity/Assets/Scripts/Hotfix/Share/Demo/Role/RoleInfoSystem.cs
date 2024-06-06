namespace ET
{
    [EntitySystemOf(typeof(RoleInfo))]
    [FriendOfAttribute(typeof(ET.RoleInfo))]
    public static partial class RoleInfoSystem
    {
        [EntitySystem]
        private static void Awake(this ET.RoleInfo self)
        {

        }

        public static void FromMessage(this RoleInfo self, RoleInfoProto roleInfoProto)
        {
            self.Name = roleInfoProto.Name;
            self.State = roleInfoProto.State;
            self.Account = roleInfoProto.Account;
            self.CreateTime = roleInfoProto.CreateTime;
            self.ServerId = roleInfoProto.ServerId;
            self.LastLoginTime = roleInfoProto.LastLoginTime;
        }

        public static RoleInfoProto ToMessage(this RoleInfo self)
        {
            RoleInfoProto roleInfoProto = RoleInfoProto.Create();
            roleInfoProto.Id = self.Id;
            roleInfoProto.Name = self.Name;
            roleInfoProto.State = self.State;
            roleInfoProto.Account = self.Account;
            roleInfoProto.CreateTime = self.CreateTime;
            roleInfoProto.ServerId = self.ServerId;
            roleInfoProto.LastLoginTime = self.LastLoginTime;

            return roleInfoProto;
        }
    }
}