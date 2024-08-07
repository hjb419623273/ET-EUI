using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class RankInfosComponent : Entity,IAwake,IDestroy
    {
        [BsonIgnore]
        public SortedList<EntityRef<RankInfo>, long> SortedRankInfoList = new SortedList<EntityRef<RankInfo>, long>(new RankInfoCompare());

        [BsonIgnore]
        public Dictionary<long, EntityRef<RankInfo>> RankInfosDictonary = new Dictionary<long, EntityRef<RankInfo>>();
    }

    [EnableClass]
    [FriendOfAttribute(typeof(ET.RankInfo))]
    public class RankInfoCompare : IComparer<EntityRef<RankInfo>>
    {
        public int Compare(EntityRef<RankInfo> x, EntityRef<RankInfo> y)
        {
            RankInfo a = x;
            RankInfo b = y;

            int result = b.Count - a.Count;

            if (result != 0)
            {
                return result;
            }

            if (a.Id < b.Id)
            {
                return 1;
            }

            if (a.Id > b.Id)
            {
                return -1;
            }

            return 0;
        }
    }
}