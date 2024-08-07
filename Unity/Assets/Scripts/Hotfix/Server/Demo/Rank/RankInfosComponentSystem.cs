namespace ET.Server
{
    [EntitySystemOf(typeof(RankInfosComponent))]
    [FriendOfAttribute(typeof(ET.Server.RankInfosComponent))]
    [FriendOfAttribute(typeof(ET.RankInfo))]
    public static partial class RankInfosComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.RankInfosComponent self)
        {
            Log.Info("RankInfosComponent  Awake !!");
        }
        [EntitySystem]
        private static void Destroy(this ET.Server.RankInfosComponent self)
        {
            foreach (RankInfo rankInfo in self.RankInfosDictonary.Values)
            {
                rankInfo?.Dispose();
            }
            self.RankInfosDictonary.Clear();
            self.SortedRankInfoList.Clear(); ;
        }

        public static async ETTask LoadRankInfo(this RankInfosComponent self)
        {
            var rankInfoList = await self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Query<RankInfo>(d => true,collection:"RankInfosComponent");
            foreach (RankInfo rankInfo in rankInfoList)
            {
                self.AddChild<RankInfo>();
                self.RankInfosDictonary.Add(rankInfo.UnitId, rankInfo);
                self.SortedRankInfoList.Add(rankInfo, rankInfo.UnitId);
            }
        }

        public static void AddOrUpdate(this RankInfosComponent self, Map2Rank_AddOrUpdateRankInfo message)
        {
            if (self.RankInfosDictonary.ContainsKey(message.unitId))
            {
                RankInfo oldRankInfo = self.RankInfosDictonary[message.unitId];
                if (oldRankInfo.Count == message.count)
                {
                    return;
                }
                self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Remove<RankInfo>(oldRankInfo.UnitId,oldRankInfo.Id,"RankInfosComponent").Coroutine();
                self.RankInfosDictonary.Remove(oldRankInfo.UnitId);
                self.SortedRankInfoList.Remove(oldRankInfo);
                oldRankInfo?.Dispose();
            }

            RankInfo rankInfo = self.AddChild<RankInfo>();
            rankInfo.UnitId = message.unitId;
            rankInfo.Name = message.roleName;
            rankInfo.Count = message.count;
            self.RankInfosDictonary.Add(rankInfo.UnitId, rankInfo);
            self.SortedRankInfoList.Add(rankInfo, rankInfo.UnitId);
            self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Save(rankInfo.UnitId, rankInfo,"RankInfosComponent").Coroutine();
        }
    }
}