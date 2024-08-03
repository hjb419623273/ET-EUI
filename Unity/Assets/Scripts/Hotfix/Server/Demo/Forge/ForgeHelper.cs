namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.ForgeComponent))]
    public static class ForgeHelper
    {
        public static void SyncAllProduction(Unit unit)
        {
            ForgeComponent forgeComponent = unit.GetComponent<ForgeComponent>();

            M2C_AllProductionList m2CAllProductionList = M2C_AllProductionList.Create();
            for (int i = 0; i < forgeComponent.ProductionsList.Count; i++)
            {
                Production ent = forgeComponent.ProductionsList[i];
                m2CAllProductionList.ProductionProtoList.Add(ent.ToMessage());
            }
            
            MapMessageHelper.SendToClient(unit, m2CAllProductionList);
        }
    }
}