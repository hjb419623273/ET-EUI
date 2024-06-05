using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]//挂载在scene上
    public class LoginInfoRecordComponent : Entity, IAwake, IDestroy
    {
        //
        public Dictionary<long, int> AccountLoginInfoDict = new Dictionary<long, int>();
    }   
}