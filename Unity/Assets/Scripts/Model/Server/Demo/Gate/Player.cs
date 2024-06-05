namespace ET.Server
{
    public enum PlayerState
    {
        Disconnect,     //断开连接
        Gate,           //Gata网关 还没被传送到map
        Game            //map服务器中
    }
    
    [ChildOf(typeof(PlayerComponent))]
    public sealed class Player : Entity, IAwake<string>
    {
        public string Account { get; set; }
        
        //play状态
        public PlayerState PlayerState { get; set; }    
        
        //游戏角色Id
        public long UnitId { get; set; }
    }
}