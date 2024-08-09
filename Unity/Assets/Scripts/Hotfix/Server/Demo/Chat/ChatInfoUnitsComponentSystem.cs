namespace ET.Server
{
    [EntitySystemOf(typeof(ChatInfoUnitsComponent))]
    [FriendOfAttribute(typeof(ET.Server.ChatInfoUnitsComponent))]
    public static partial class ChatInfoUnitsComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.ChatInfoUnitsComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Server.ChatInfoUnitsComponent self)
        {
            foreach (var chatInfoUnitEnt in self.ChatInfoUnitsDict.Values)
            {
                ChatInfoUnit chatInfoUnit = chatInfoUnitEnt;
                chatInfoUnit?.Dispose();
            }
        }

        public static void Add(this ChatInfoUnitsComponent self, ChatInfoUnit chatInfoUnit)
        {
            if (!self.ChatInfoUnitsDict.TryAdd(chatInfoUnit.Id, chatInfoUnit))
            {
                Log.Error($"chatInfoUnit is exist! ： {chatInfoUnit.Id}");
                return;
            }
        }

        public static ChatInfoUnit Get(this ChatInfoUnitsComponent self, long id)
        {
            self.ChatInfoUnitsDict.TryGetValue(id, out EntityRef<ChatInfoUnit> chatInfoUnit);
            return chatInfoUnit;
        }

        public static void Remove(this ChatInfoUnitsComponent self, long id)
        {
            if (self.ChatInfoUnitsDict.Remove(id, out EntityRef<ChatInfoUnit> chatInfoUnitEnt))
            {
                ChatInfoUnit chatInfoUnit = chatInfoUnitEnt;
                chatInfoUnit?.Dispose();;
            }
        }
    }
}