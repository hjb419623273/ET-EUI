using System;

namespace ET.Client
{
    public static class RankHelper
    {
        public static async ETTask<int> GetRankInfo(Scene scene)
        {
            Rank2C_GetRanksInfo rank2CGetRanksInfo = null;
            try
            {
                rank2CGetRanksInfo = await scene.GetComponent<ClientSenderComponent>().Call(C2Rank_GetRanksInfo.Create()) as Rank2C_GetRanksInfo;
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return ErrorCode.ERR_NetWorkError;
            }

            if (rank2CGetRanksInfo.Error != ErrorCode.ERR_Success)
            {
                return rank2CGetRanksInfo.Error;
            }
            
            scene.GetComponent<RankComponent>().ClearAll();
            if (rank2CGetRanksInfo.RankInfoProtoList != null) 
            {
                for (int i = 0; i < rank2CGetRanksInfo.RankInfoProtoList.Count; i++)
                {
                    scene.GetComponent<RankComponent>().Add(rank2CGetRanksInfo.RankInfoProtoList[i]);
                }
            }

            return rank2CGetRanksInfo.Error;
        }
    }
}