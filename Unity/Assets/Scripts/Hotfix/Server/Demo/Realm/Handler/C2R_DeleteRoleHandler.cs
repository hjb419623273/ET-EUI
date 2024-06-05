using System.Collections.Generic;

namespace ET.Server
 {
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOfAttribute(typeof(ET.RoleInfo))]
    public class C2R_DeleteRoleHandler : MessageSessionHandler<C2R_DeleteRole, R2C_DeleteRole>
    {
        protected override async ETTask Run(Session session, C2R_DeleteRole request, R2C_DeleteRole response)
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

            CoroutineLockComponent coroutineLockComponent = session.Root().GetComponent<CoroutineLockComponent>();

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await coroutineLockComponent.Wait(CoroutineLockType.CreateRole, request.Account.GetLongHashCode()))
                {
                    DBComponent dbComponent = session.Root().GetComponent<DBManagerComponent>().GetZoneDB(session.Zone());
                    
                    List<RoleInfo> roleInfos = await dbComponent.Query<RoleInfo>(d => d.Id == request.RoleInfoId && d.ServerId == request.ServerId);

                    if (roleInfos == null || roleInfos.Count <= 0)
                    {
                        response.Error = ErrorCode.ERR_RoleNotExist;
                        return;
                    }

                    var roleInfo = roleInfos[0];
                    session.AddChild(roleInfo);     //这里实体创建出来必须添加到实体树中

                    roleInfo.State = (int)RoleInfoState.Freeze;     //冻结状态   防止用户找回账号数据
                    await dbComponent.Save(roleInfo);
                    response.DeletedRoleInfoId = roleInfo.Id;
                    roleInfo.Dispose();                 //手动释放
                }
            }
        }
    }
}