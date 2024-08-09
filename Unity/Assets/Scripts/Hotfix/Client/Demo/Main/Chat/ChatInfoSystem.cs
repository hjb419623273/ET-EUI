namespace ET.Client
{
    [EntitySystemOf(typeof(ChatInfo))]
    [FriendOfAttribute(typeof(ET.Client.ChatInfo))]
    public static partial class ChatInfoSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.ChatInfo self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.ChatInfo self)
        {
            self.Message = null;
            self.Name = null;
        }
    }
}