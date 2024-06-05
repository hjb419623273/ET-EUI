namespace ET.Server
{
    [MessageSessionHandler(SceneType.Realm)]
    [FriendOfAttribute(typeof(ET.Server.ServerInfoManagerComponent))]
    public class C2R_GetServerInfosHandler : MessageSessionHandler<C2R_GetServerInfos, R2C_GetServerInfos>
    {
        protected override async ETTask Run(Session session, C2R_GetServerInfos request, R2C_GetServerInfos response)
        {
            string token = session.Root().GetComponent<TokenComponent>().Get(request.Account);

            if (token == null || token != request.Token)
            {
                response.Error = ErrorCode.ERR_TokenError;
                session?.Disconnect().Coroutine();
                return;
            }

            foreach (var serverInfoRef in session.Root().GetComponent<ServerInfoManagerComponent>().ServerInfos)
            {
                ServerInfo serverInfo = serverInfoRef;
                response.ServerInfosList.Add(serverInfo.ToMessage());
            }

            await ETTask.CompletedTask;
        }
    }
}