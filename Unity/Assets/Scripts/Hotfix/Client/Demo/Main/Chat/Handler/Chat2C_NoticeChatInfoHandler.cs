namespace ET.Client
{
    [MessageHandler(SceneType.Demo)]
    [FriendOfAttribute(typeof(ET.Client.ChatInfo))]
    public class Chat2C_NoticeChatInfoHandler : MessageHandler<Scene, Chat2C_NoticeChatInfo>
    {
        protected override async ETTask Run(Scene scene, Chat2C_NoticeChatInfo message)
        {
            ChatInfo chatInfo = scene.GetComponent<ChatComponent>().AddChild<ChatInfo>(true);
            chatInfo.Name = message.Name;
            chatInfo.Message = message.ChatMessage;
            scene.GetComponent<ChatComponent>().Add(chatInfo);
            EventSystem.Instance.Publish(scene, new UpdateChatInfo() { Scene = scene });
            await ETTask.CompletedTask;
        }
    }
}