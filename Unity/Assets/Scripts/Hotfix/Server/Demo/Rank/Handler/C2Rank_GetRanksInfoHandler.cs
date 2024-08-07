namespace ET.Server
{
    [MessageHandler(SceneType.Rank)]
    [FriendOfAttribute(typeof(ET.Server.RankInfosComponent))]
    public class C2Rank_GetRanksInfoHandler : MessageHandler<Scene, C2Rank_GetRanksInfo, Rank2C_GetRanksInfo>
    {
        protected override async ETTask Run(Scene scene, C2Rank_GetRanksInfo request, Rank2C_GetRanksInfo response)
        {
            RankInfosComponent rankInfosComponent = scene.GetComponent<RankInfosComponent>();
            int count = 0;
            if (rankInfosComponent.SortedRankInfoList != null)
            {
                foreach (var rankInfo in rankInfosComponent.SortedRankInfoList)
                {
                    RankInfo rankInfoTmp = rankInfo.Key;
                    response.RankInfoProtoList.Add(rankInfoTmp.ToMessage());
                    ++count;
                    if (count >= 100)
                    {
                        break;
                    }
                }
            }

            await ETTask.CompletedTask;
        }
    }
}