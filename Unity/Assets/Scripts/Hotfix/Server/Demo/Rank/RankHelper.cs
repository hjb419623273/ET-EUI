namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.UnitRoleInfo))]
    public static class RankHelper
    {
        public static void AddOrUpdateLevelRank(Unit unit)
        {
            Map2Rank_AddOrUpdateRankInfo message = Map2Rank_AddOrUpdateRankInfo.Create();
            message.unitId = unit.Id;
            message.roleName = unit.GetComponent<UnitRoleInfo>().Name;

            message.count = unit.GetComponent<NumericComponent>().GetAsInt(NumericType.Level);
            ActorId instanceId = StartSceneConfigCategory.Instance.GetBySceneName(unit.Zone(), "Rank").ActorId;
            unit.Root().GetComponent<MessageSender>().Send(instanceId, message);
        }
    }
}