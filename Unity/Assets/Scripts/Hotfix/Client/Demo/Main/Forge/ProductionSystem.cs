using System;

namespace ET.Client
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

        public static bool IsMakingState(this Production self)
        {
            return self.ProductionState == (int)ProductionState.Making;
        }

        public static bool IsMakeTimeOver(this Production self)
        {
            return self.TargetTime <= TimeInfo.Instance.ServerNow();
        }

        public static float GetRemainTimeValue(this Production self)
        {
            long RemainTime = self.TargetTime - TimeInfo.Instance.ServerNow();
            if (RemainTime <= 0)
            {
                return 0.0f;
            }

            long totalTIme = self.TargetTime - self.StartTime;

            return RemainTime / (float)totalTIme;
        }

        public static string GetRemainingTimeStr(this Production self)
        {
            long RemainTime = self.TargetTime - TimeInfo.Instance.ServerNow();
            if (RemainTime <= 0)
            {
                return "0时0分0秒";
            }

            RemainTime /= 1000;

            float h = MathF.Floor(RemainTime / 3600f);
            float m = MathF.Floor(RemainTime / 60f - h * 60f);
            float s = MathF.Floor(RemainTime - m * 60f - h * 3600f);
            return h.ToString("00") + "小时" + m.ToString("00") + "分" + s.ToString("00") + "秒";
        }

        public static void FromMessage(this Production self, ProductionProto productionProto)
        {
            self.ConfigId = productionProto.ConfigId;
            self.ProductionState = productionProto.ProductionState;
            self.StartTime = productionProto.StartTime;
            self.TargetTime = productionProto.TargetTime;
        }
    }
    
}