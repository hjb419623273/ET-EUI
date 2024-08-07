using System.Collections.Generic;
namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class RankComponent : Entity, IAwake, IDestroy
    {
        public List<EntityRef<RankInfo>> RankInfos = new List<EntityRef<RankInfo>>(100);
    }
}