namespace ET.Server
{
    [EntitySystemOf(typeof(Production))]
    [FriendOfAttribute(typeof(ET.Production))]
    public static partial class ProductionSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Production self)
        {
        
        }
        
        [EntitySystem]
        private static void Destroy(this ET.Production self)
        {
        
        }
        
        public static ProductionProto ToMessage(this Production self)
        {
            ProductionProto productionProto = ProductionProto.Create();
            productionProto.Id = self.Id;
            productionProto.ConfigId = self.ConfigId;
            productionProto.StartTime = self.StartTime;
            productionProto.TargetTime = self.TargetTime;
            productionProto.ProductionState = self.ProductionState;
            return productionProto;
        }

        public static void Reset(this Production self)
        {
            self.ConfigId = 0;
            self.ProductionState = (int)ProductionState.Received;
            self.TargetTime = 0;
            self.StartTime = 0;
        }

    }
}