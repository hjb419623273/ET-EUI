namespace ET.Server
{
    [MessageHandler(SceneType.ChatInfo)]
    [FriendOfAttribute(typeof(ET.Server.ChatInfoUnit))]
    public class G2Chat_EnterChatHandler : MessageHandler<Scene, G2Chat_EnterChat, Chat2G_EnterChat>
    {
        protected override async ETTask Run(Scene scene, G2Chat_EnterChat request, Chat2G_EnterChat response)
        {
            ChatInfoUnitsComponent chatInfoUnitsComponent = scene.GetComponent<ChatInfoUnitsComponent>();
            ChatInfoUnit chatInfoUnit = chatInfoUnitsComponent.Get(request.UnitId);

            if (chatInfoUnit != null && !chatInfoUnit.IsDisposed)
            {
                chatInfoUnit.Name = request.Name;
                chatInfoUnit.PlayerSessionComponentActorId = request.PlayerSessionComponentActorId;
                response.ChatInfoUnitActorId = chatInfoUnit.GetActorId();
                return;
            }

            chatInfoUnit = chatInfoUnitsComponent.AddChildWithId<ChatInfoUnit>(request.UnitId);
            chatInfoUnit.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            await chatInfoUnit.AddLocation(LocationType.Chat);

            chatInfoUnit.Name = request.Name;
            chatInfoUnit.PlayerSessionComponentActorId = request.PlayerSessionComponentActorId;
            chatInfoUnitsComponent.Add(chatInfoUnit);
            
            response.ChatInfoUnitActorId = chatInfoUnit.GetActorId();
            await ETTask.CompletedTask;
        }
    }
}