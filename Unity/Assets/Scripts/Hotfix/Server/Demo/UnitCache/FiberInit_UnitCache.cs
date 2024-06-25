using System.Collections.Generic;

namespace ET.Server
{
    [Invoke((long)SceneType.UnitCache)]
    public class FiberInit_UnitCache: AInvokeHandler<FiberInit, ETTask>
    {
        public override async ETTask Handle(FiberInit fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            root.AddComponent<MailBoxComponent, MailBoxType>(MailBoxType.UnOrderedMessage);
            root.AddComponent<TimerComponent>();
            root.AddComponent<CoroutineLockComponent>();
            root.AddComponent<ProcessInnerSender>();
            root.AddComponent<MessageSender>();
            
            root.AddComponent<UnitCacheComponent>();
            root.AddComponent<DBManagerComponent>();
            
            // root.AddComponent<TimerComponent>();
            // root.AddComponent<LocationProxyComponent>();
            // root.AddComponent<MessageLocationSenderComponent>();
            
            await ETTask.CompletedTask;
        }
    }
}