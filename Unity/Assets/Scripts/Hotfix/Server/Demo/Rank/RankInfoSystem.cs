namespace ET.Server
{
    [EntitySystemOf(typeof(RankInfo))]
    [FriendOfAttribute(typeof(ET.RankInfo))]
    public static partial class RankInfoSystem
    {
        [EntitySystem]
        private static void Awake(this ET.RankInfo self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.RankInfo self)
        {
            self.UnitId = 0;
            self.Name = null;
            self.Count = 0;
        }

        public static void FromMessage(this RankInfo self, RankInfoProto rankInfoProto)
        {
            self.UnitId = rankInfoProto.UnitId;
            self.Name = rankInfoProto.Name;
            self.Count = rankInfoProto.Count;
        }

        public static RankInfoProto ToMessage(this RankInfo self)
        {
            RankInfoProto rankInfoProto = RankInfoProto.Create();
            rankInfoProto.Id = self.Id;
            rankInfoProto.UnitId = self.UnitId;
            rankInfoProto.Name = self.Name;
            rankInfoProto.Count = self.Count;
            return rankInfoProto;
        }
    }
}