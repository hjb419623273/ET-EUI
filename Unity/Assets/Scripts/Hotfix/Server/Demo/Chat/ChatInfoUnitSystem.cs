namespace ET.Server
{
    [EntitySystemOf(typeof(ChatInfoUnit))]
    [FriendOfAttribute(typeof(ET.Server.ChatInfoUnit))]
    public static partial class ChatInfoUnitSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.ChatInfoUnit self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Server.ChatInfoUnit self)
        {
            self.Name = null;
        }
    }
}