namespace ET.Client
{
    [EntitySystemOf(typeof(ChatComponent))]
    [FriendOfAttribute(typeof(ET.Client.ChatComponent))]
    public static partial class ChatComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.ChatComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.ChatComponent self)
        {
            foreach (var chatInfoEt in self.ChatMessageQueue)
            {
                ChatInfo chatInfo = chatInfoEt;
                if (chatInfo != null)
                {
                    chatInfo?.Dispose();;
                }
            }
            self.ChatMessageQueue.Clear();
        }

        public static void Add(this ChatComponent self,ChatInfo chatInfo)
        {
            //最多100条
            if (self.ChatMessageQueue.Count >= 100)
            {
                ChatInfo oldChatInfo = self.ChatMessageQueue.Dequeue();
                oldChatInfo?.Dispose();
            }
            self.ChatMessageQueue.Enqueue(chatInfo);
        }

        public static int GetChatMessageCount(this ChatComponent self)
        {
            return self.ChatMessageQueue.Count;
        }

        public static ChatInfo GetChatMessageByIndex(this ChatComponent self, int index)
        {
            int tempIndex = 0;
            foreach (var chatInfoEnt in self.ChatMessageQueue)
            {
                ChatInfo chatInfo = chatInfoEnt;
                if (tempIndex == index)
                {
                    return chatInfo;
                }

                ++tempIndex;
            }

            return null;
        }
    }
}