using System;
using System.Collections.Generic;
using System.IO;

namespace ET.Client
{
    [Event(SceneType.Main)]
    public class EntryEvent3_InitClient: AEvent<Scene, EntryEvent3>
    {
        protected override async ETTask Run(Scene root, EntryEvent3 args)
        {
            GlobalComponent globalComponent = root.AddComponent<GlobalComponent>();
            root.AddComponent<UIGlobalComponent>();
            root.AddComponent<UIPathComponent>();
            root.AddComponent<UIEventComponent>();
            root.AddComponent<UIComponent>();
            root.AddComponent<ResourcesLoaderComponent>();
            root.AddComponent<PlayerComponent>();
            root.AddComponent<CurrentScenesComponent>();
            
            root.AddComponent<RecordAccountInfoComponect>();    //记录服务账号信息组件
            root.AddComponent<ClientServerInfosComponent>();    //记录服务器信息组件
            root.AddComponent<RoleInfosComponent>();            //记录角色信息 
            root.AddComponent<BagComponent>();                  //背包组件                  
            root.AddComponent<EquipmentsComponent>();           
            root.AddComponent<ForgeComponent>();                //打造

            await root.AddComponent<RedDotComponent>().PreLoadGameObject();
            
            // 根据配置修改掉Main Fiber的SceneType
            SceneType sceneType = EnumHelper.FromString<SceneType>(globalComponent.GlobalConfig.AppType.ToString());
            root.SceneType = sceneType;
            
            await EventSystem.Instance.PublishAsync(root, new AppStartInitFinish());
        }
    }
}