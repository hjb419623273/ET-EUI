namespace ET.Server
{
    [EntitySystemOf(typeof(ForgeComponent))]
    [FriendOfAttribute(typeof(ET.Server.ForgeComponent))]
    [FriendOfAttribute(typeof(ET.Production))]
    public static partial class ForgeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.ForgeComponent self)
        {
            Log.Info("server ForgeComponent awake!");
        }
        [EntitySystem]
        private static void Destroy(this ET.Server.ForgeComponent self)
        {
            Log.Info("server ForgeComponent Destroy!");
        }
        [EntitySystem]
        private static void Deserialize(this ET.Server.ForgeComponent self)
        {
            Log.Info("server ForgeComponent Deserialize!");
            foreach (var entity in self.Children.Values)
            {
                self.ProductionsList.Add(entity as Production);
            }
        }

        public static Production GetProductionById(this ForgeComponent self, long productionId)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production ent = self.ProductionsList[i];
                if (ent.Id == productionId)
                {
                    return ent;
                }
            }
            return null;
        }

        public static bool IsExistProducitonOverQueue(this ForgeComponent self, long productionId)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production ent = self.ProductionsList[i];
                if (ent.Id == productionId 
                    && ent.ProductionState == (int)ProductionState.Making 
                    && ent.TargetTime <= TimeInfo.Instance.ServerNow())
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsExistFreeQueue(this ForgeComponent self)
        {
            if (self.ProductionsList.Count < 2)
            {
                return true;
            }

            Production production = self.GetFreeFroduction();
            if (production != null)
            {
                return true;
            }

            return false;
        }
        
        public static Production StartProduction(this ForgeComponent self, int configId)
        {
            Production production = self.GetFreeFroduction();
            if (production == null)
            {
                production = self.AddChild<Production>();
                self.ProductionsList.Add(production);
            }

            var child = self.Children;
            production.ConfigId        = configId;
            production.ProductionState = (int)ProductionState.Making;
            production.StartTime       = TimeInfo.Instance.ServerNow();
            production.TargetTime      = TimeInfo.Instance.ServerNow() + (ForgeProductionConfigCategory.Instance.Get(configId).ProductionTime * 1000);

            return production;
        }

        public static Production GetFreeFroduction(this ForgeComponent self)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production ent = self.ProductionsList[i];
                if (ent.ProductionState == (int)ProductionState.Received)
                {
                    return ent;
                }
            }

            return null;
        }
    }
}