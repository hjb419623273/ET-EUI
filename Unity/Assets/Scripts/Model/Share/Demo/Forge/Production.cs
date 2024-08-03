namespace ET
{
    public enum ProductionComsumType
    {
        IronStone = 1, 
        Fur       = 2,
    }

    public enum ProductionState
    {
        Received = 0, //已领取
        Making = 1,    //正在制作
    }
    
    [ChildOf]
    public class Production : Entity, IAwake, IDestroy,ISerializeToEntity
    {
        public long StartTime = 0;
        public long TargetTime = 0;
        public int ConfigId = 0;
        public int ProductionState = 0;
    }
}