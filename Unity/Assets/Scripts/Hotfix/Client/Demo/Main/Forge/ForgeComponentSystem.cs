using System.Collections.Generic;

namespace ET.Client
{
    [Invoke(TimerInvokeType.MakeQueueOver)]
    public class MakeQueueOverTimer : ATimer<ForgeComponent>
    {
        protected override void Run(ForgeComponent t)
        {
            EventSystem.Instance.Publish(t.Root(), MakeQueueOver.Instance);
        }
    }

    [EntitySystemOf(typeof(ForgeComponent))]
    [FriendOfAttribute(typeof(ET.Client.ForgeComponent))]
    [FriendOfAttribute(typeof(ET.Production))]
    public static partial class ForgeComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Client.ForgeComponent self)
        {

        }
        [EntitySystem]
        private static void Destroy(this ET.Client.ForgeComponent self)
        {
            foreach (Production production in self.ProductionsList)
            {
                production?.Dispose();
            }

            foreach (var kv in self.ProductionTimerDict)
            {
                long value = kv.Value;
                self.Root().GetComponent<TimerComponent>()?.Remove(ref value);
            }   
        }

        public static bool IsExistMakeQueueOver(this ForgeComponent self)
        {
            bool isCanRecive = false;
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production production = self.ProductionsList[i];
                if (production.IsMakingState() && production.IsMakeTimeOver())
                {
                    isCanRecive = true;
                    break;
                }
            }

            return isCanRecive;
        }

        public static void AddOrUpdateProductionQueue(this ForgeComponent self, ProductionProto productionProto)
        {
            Production production = self.GetProductionById(productionProto.Id);

            if (production == null)
            {
                // production = self.AddChild<Production>();
                production = self.AddChildWithId<Production>(productionProto.Id);
                self.ProductionsList.Add(production);
            }
            production.FromMessage(productionProto);

            if (self.ProductionTimerDict.TryGetValue(production.Id, out long timeId))
            {
                self.Root().GetComponent<TimerComponent>().Remove(ref timeId);
                self.ProductionTimerDict.Remove(production.Id);
            }

            if (production.IsMakingState() && !production.IsMakeTimeOver())
            {
                Log.Debug($"启动一个定时器！！！:{production.TargetTime}");
                timeId = self.Root().GetComponent<TimerComponent>().NewOnceTimer(production.TargetTime, TimerInvokeType.MakeQueueOver, self);
                self.ProductionTimerDict.Add(production.Id, timeId);
            }
            EventSystem.Instance.Publish(self.Root(), MakeQueueOver.Instance);
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

        public static Production GetProductionByIndex(this ForgeComponent self, int index)
        {
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                if (i == index)
                {
                    return self.ProductionsList[i];
                }
            }
            return null;
        }

        public static int GetMakeingProductionQueueCount(this ForgeComponent self)
        {
            int count = 0;
            for (int i = 0; i < self.ProductionsList.Count; i++)
            {
                Production production = self.ProductionsList[i];
                if (production.ProductionState == (int)ProductionState.Making)
                {
                    ++count;
                }
            }

            return count;
        }
    }
}