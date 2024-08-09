using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class ChatComponent : Entity,IAwake,IDestroy
    {
        public Queue<EntityRef<ChatInfo>> ChatMessageQueue = new Queue<EntityRef<ChatInfo>>(100);
    }
}