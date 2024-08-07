namespace ET.Server
{
    [MessageHandler(SceneType.LoginCenter)]
    public class G2L_GetRoleInfoHandler  : MessageHandler<Scene,G2L_GetRoleInfo,L2G_GetRoleInfo>
    {
        protected override async ETTask Run(Scene scene, G2L_GetRoleInfo request, L2G_GetRoleInfo response)
        {
            long accountId = request.AccountName.GetLongHashCode();
            int zone = scene.Zone();
            long roleId = request.RoleId;
            var roleInfos = await scene.GetComponent<DBManagerComponent>().GetZoneDB(zone).Query<RoleInfo>(d => d.Id == roleId);
            foreach (var roleInfo in roleInfos)
            {
                response.RoleInfo.Add(roleInfo.ToMessage()); 
                roleInfo?.Dispose();
            }
            roleInfos.Clear();
            await ETTask.CompletedTask;
        }
    }
}