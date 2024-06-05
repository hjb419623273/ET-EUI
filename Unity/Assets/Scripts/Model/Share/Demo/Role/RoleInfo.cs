namespace ET
{
    public enum RoleInfoState
    {
        Normal = 0,     
        Freeze,     //冻结章台
    }
    
    [ChildOf(typeof(Session))]
    public class RoleInfo : Entity, IAwake
    {
        public string Name;         //角色名称

        public int ServerId;        //

        public int State;

        public string Account;

        public long LastLoginTime;

        public long CreateTime;
    }
}
