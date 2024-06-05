using System.Runtime.InteropServices.WindowsRuntime;

namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOfAttribute(typeof(ET.RoleInfo))]
    public class C2R_CreateRoleHandler : MessageSessionHandler<C2R_CreateRole, R2C_CreateRole>
    {
        protected override async ETTask Run(Session session, C2R_CreateRole request, R2C_CreateRole response)
        {
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                response.Error = ErrorCode.ERR_RequestRepeatedly;
                session.Disconnect().Coroutine();
                return;
            }

            string token = session.Root().GetComponent<TokenComponent>().Get(request.Account);

            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                session?.Disconnect().Coroutine();
                return;
            }

            if (string.IsNullOrEmpty(request.Name))
            {
                response.Error = ErrorCode.ERR_RoleNameIsNull;
                return;
            }

            CoroutineLockComponent coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await coroutineLockComponent.Wait(CoroutineLockType.CreateRole, request.Account.GetLongHashCode()))
                {
                    DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                    
                    //查询是否重名
                    var roleInfos = await dbComponent.Query<RoleInfo>(d => d.Name == request.Name && d.ServerId == request.ServerId);
                    if (roleInfos != null && roleInfos.Count > 0)
                    {
                        response.Error = ErrorCode.ERR_RoleNameSame;
                        return;
                    }

                    RoleInfo newRoleInfo = session.AddChild<RoleInfo>();
                    newRoleInfo.Name = request.Name;
                    newRoleInfo.State = (int)RoleInfoState.Normal;
                    newRoleInfo.ServerId = request.ServerId;
                    newRoleInfo.Account = request.Account;
                    newRoleInfo.CreateTime = TimeInfo.Instance.ServerNow();
                    newRoleInfo.LastLoginTime = 0;

                    await dbComponent.Save<RoleInfo>(newRoleInfo);

                    response.RoleInfo = newRoleInfo.ToMessage();
                     
                    newRoleInfo?.Dispose();
                }
            }
        }
    }
}