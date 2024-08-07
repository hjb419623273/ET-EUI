namespace ET.Client
{
    [EntitySystemOf(typeof(RankComponent))]
    [FriendOfAttribute(typeof(ET.Client.RankComponent))]
    public static partial class RankComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.RankComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.RankComponent self)
        {

        }

        public static void ClearAll(this RankComponent self)
        {
            for (int i = 0; i < self.RankInfos.Count; i++)
            {
                RankInfo info = self.RankInfos[i];
                info?.Dispose();
            }

            self.RankInfos.Clear();
        }

        public static void Add(this RankComponent self,RankInfoProto rankInfoProto)
        {
            RankInfo rankInfo = self.AddChild<RankInfo>(true);
            rankInfo.FromMessage(rankInfoProto);
            self.RankInfos.Add(rankInfo);
        }

        public static int GetRankCount(this RankComponent self)
        {
            return self.RankInfos.Count;
        }

        public static RankInfo GetRankInfoByIndex(this RankComponent self, int index)
        {
            if (index < 0 || index >= self.RankInfos.Count)
            {
                return null;
            }

            return self.RankInfos[index];
        }
    }
}