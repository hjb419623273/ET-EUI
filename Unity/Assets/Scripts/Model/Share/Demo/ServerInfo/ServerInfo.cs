namespace ET
{
    public enum ServerStatus
    {
        Normal = 0,     //正常
        Stop = 1,       //停服维护
    }
    
    [ChildOf]
    public class ServerInfo : Entity, IAwake
    {
        public int Status;          //服务器状态
        public string ServerName;
    }
}
