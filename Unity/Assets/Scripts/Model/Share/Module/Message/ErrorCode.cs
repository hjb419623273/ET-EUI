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

        public const int ERR_RequestRepeatedly = 200001;

        public const int ERR_LoginInfoIsNull = 200002;

        public const int ERR_AccountNameFormError = 200003;

        public const int ERR_PasswordFormError = 200004;

        public const int ERR_AccountInBlackListError = 200005;

        public const int ERR_LoginPasswordError = 200006;


        public const int ERR_OtherAccountLogin = 200007;
        
        public const int ERR_LoginGameGateError01 = 200008;

        public const int ERR_SessionPlayerError = 200009;

        public const int ERR_NonePlayerError = 200010;

        public const int ERR_ReEnterGameError2 = 200011;

        public const int ERR_PlayerSessionError = 200012;


        public const int ERR_ReEnterGameError = 200013;

        public const int ERR_EnterGameError = 200014;


        public const int ERR_TokenError = 200015;

        public const int ERR_RoleNameIsNull = 200016;

        public const int ERR_RoleNameSame = 200017;

        public const int ERR_RoleNotExist = 200018;

        public const int ERR_NetWorkError = 2000019;
        
        public const int ERR_NumericTypeNotExist = 2000020;         //表中不存在该属性
        
        public const int ERR_NumericTypeNotAddPoint = 2000021;      //该属性不能用属性点添加
        
        public const int ERR_AddPointNotEnough = 2000022;
        
        public const int ERR_AdventureLevelIdError      = 2000023;
        
        public const int ERR_AdventureRoundError        = 2000024;
        
        public const int ERR_AdventureResultError       = 2000025;
        
        public const int ERR_AdventureWinResultError    = 2000026;
        
        public const int ERR_AlreadyAdventureState      = 2000027;
        
        public const int ERR_AdventureInDying           = 2000028;
        
        public const int ERR_AdventureErrorLevel        = 2000029;
        
        public const int ERR_AdventureLevelNotEnough    = 2000030;
        
        public const int ERR_ExpNotEnough               = 2000031;
        public const int ERR_ExpNumError                = 2000032;
        
        //道具不存在
        public const int ERR_ItemNotExist               = 2000033;
        
        public const int ERR_AddBagItemError            = 2000034;
        
        public const int ERR_EquipItemError             = 2000035;
        
        public const int ERR_BagMaxLoad                 = 2000036;

        public const int ERR_MakeConfigNotExist         = 2000037;

        public const int ERR_MakeConsumeError           = 2000038;
        
        public const int ERR_NoMakeQueueOver            = 2000039;
        
        public const int ERR_NoMakeFreeQueue            = 2000040;
        
    }
}