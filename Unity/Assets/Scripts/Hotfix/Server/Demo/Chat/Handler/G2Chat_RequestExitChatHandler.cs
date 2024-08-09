namespace ET.Server
{
    [MessageHandler(SceneType.ChatInfo)]
    public class G2Chat_RequestExitChatHandler : MessageHandler<ChatInfoUnit, G2Chat_RequestExitChat,Chat2G_RequestExitChat>
    {
        protected override async ETTask Run(ChatInfoUnit chatInfoUnit, G2Chat_RequestExitChat request, Chat2G_RequestExitChat response)
        {
            ChatInfoUnitsComponent chatInfoUnitsComponent = chatInfoUnit.Root().GetComponent<ChatInfoUnitsComponent>();
            chatInfoUnitsComponent.Remove(chatInfoUnit.Id);
            await ETTask.CompletedTask;
        }
    }
}