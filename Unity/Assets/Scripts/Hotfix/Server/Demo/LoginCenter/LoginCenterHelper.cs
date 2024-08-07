namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.UnitRoleInfo))]
    public static class LoginCenterHelper
    {
        public static async ETTask GetRoleInfo(Player player, Unit unit)
        {
            G2L_GetRoleInfo g2LGetRoleInfo = G2L_GetRoleInfo.Create();
            g2LGetRoleInfo.AccountName = player.Account;
            g2LGetRoleInfo.RoleId = player.UnitId;

            var L2G_RemoveLoginRecord = await player.Root().GetComponent<MessageSender>()
                    .Call(StartSceneConfigCategory.Instance.LoginCenterConfig.ActorId, g2LGetRoleInfo) as L2G_GetRoleInfo;

            RoleInfoProto roleInfoProto = L2G_RemoveLoginRecord.RoleInfo[0];
            UnitRoleInfo unitRoleInfo = unit.GetComponent<UnitRoleInfo>();
            unitRoleInfo.Account = roleInfoProto.Account;
            unitRoleInfo.Name = roleInfoProto.Name;
            
            await ETTask.CompletedTask;
        }
    }
}