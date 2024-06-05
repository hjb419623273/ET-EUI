namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误
        
        // 110000以下的错误请看ErrorCore.cs
        
        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常
        public const int ERR_RequestRepeatedly = 200001;            //重复进行登录请求
        
        public const int ERR_LoginInfoIsNull = 200002;              //登录时账号为空

        public const int ERR_AccountNameFormError = 200003;        //账号格式

        public const int ERR_PasswordFormError = 200004;           //密码格式
        
        public const int ERR_LoginPasswordError = 200005;

        public const int ERR_AccountInBlackListErroer = 200006;     //账号在黑名单中
        
        public const int ERR_PasswordError = 200007;

        public const int ERR_TokenError = 200008;                 //token错误or过期
        
        public const int ERR_RoleNameIsNull = 200009;  
        
        public const int ERR_RoleNameSame = 200010;  
        
        public const int ERR_RoleNotExist = 200011;             //删除账号时账号为空 

        public const int ERR_ConnectGateKeyError = 200012;      //连接gate服务器令牌错误
        
        public const int ERR_LoginGameGateError01 = 200013;     
        
        public const int ERR_LoginGameGateError = 200014;    
        
        public const int ERR_OtherAccountLogin = 200015;        //顶号操作踢人下线
        
        public const int ERR_SessionPlayerError = 200016;       //客户端为经过gate网关直接请求进入游戏服
        
        public const int ERR_NonePlayerError = 200017;   
        
        public const int ERR_PlayerSessionError = 200018;
        
        public const int ERR_EnterGameError = 200019;
        
        public const int ERR_ReEnterGameError = 200020;
        
        public const int ERR_ReEnterGameError2 = 200021;
    }
}