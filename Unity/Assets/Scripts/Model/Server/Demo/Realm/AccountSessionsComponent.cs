using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]   //挂载在Scene实体上
    public class AccountSessionsComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<string, EntityRef<Session>> AccountSessionDictionary = new Dictionary<string, EntityRef<Session>>();
    }
}