using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf]
    
    public class ForgeComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<long, long> ProductionTimerDict = new Dictionary<long, long>();
        //生产队列
        public List<EntityRef<Production>> ProductionsList = new List<EntityRef<Production>>();
    }
}