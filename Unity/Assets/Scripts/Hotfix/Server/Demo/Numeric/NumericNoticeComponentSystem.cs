namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.NumericNoticeComponent))]
    public static class NumericNoticeComponentSystem
    {
        public static void NoticeImmediately(this NumericNoticeComponent self, NumbericChange args)
        {
            Unit unit = self.GetParent<Unit>();
            // 多线程网络 使用缓存一次发送多个消息，导致被覆盖  这里根据需求修
            // self.NoticeUnitNumericMessage.UnitId = unit.Id;
            // self.NoticeUnitNumericMessage.NumericType = args.NumericType;
            // self.NoticeUnitNumericMessage.NewValue = args.New;


            M2C_NoticeUnitNumeric NoticeUnitNumericMessage = M2C_NoticeUnitNumeric.Create();
            NoticeUnitNumericMessage.UnitId = unit.Id;
            NoticeUnitNumericMessage.NumericType = args.NumericType;
            NoticeUnitNumericMessage.NewValue = args.New;
            Log.Warning(">>>>>>>>server NumericType:" + args.NumericType + " value:" + args.New);

            MapMessageHelper.SendToClient(unit, NoticeUnitNumericMessage);
        }
    }
}
