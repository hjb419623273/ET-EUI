namespace ET.Server
{
    //登录中心服 初始化fiber
    [Invoke((long)SceneType.LoginCenter)]
    public class FiberInit_LoginCenter : AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();        //携程锁组件
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<MessageSender>();

            root.AddComponent<LoginInfoRecordComponent>();
            await ETTask.CompletedTask;
        }
    }
    
}

