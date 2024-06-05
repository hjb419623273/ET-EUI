namespace ET.Server
{
    public enum AccountType
    {
        General = 0,        //普通账号
            
        BlackList = 1,      //黑名单
    }
    
    [ChildOf(typeof(Session))]
    public class Account : Entity, IAwake
    {
        public string AccountName; //账户名

        public string Password;    //账户密码

        public long CreateTime;    //账号创建时间

        public int AccountType;    //账号类型
    }
}