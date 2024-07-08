using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.UnitDBSaveComponent))]
    public class UnitDBSaveComponentAwakeSystem : AwakeSystem<UnitDBSaveComponent>
    {
        protected override void Awake(UnitDBSaveComponent self)
        {
            //启动一个定时器
            //正式项目设置为5~10min保存一次
            self.Timer = self.Root().GetComponent<TimerComponent>().NewRepeatedTimer(10000, TimerInvokeType.SaveChangeDBData, self);
        }
    }
    [FriendOfAttribute(typeof(ET.Server.UnitDBSaveComponent))]
    public class UnitDBSaveComponentDestorySystem : DestroySystem<UnitDBSaveComponent>
    {
        protected override void Destroy(UnitDBSaveComponent self)
        {
            self.Root().GetComponent<TimerComponent>().Remove(ref self.Timer);
        }
    }

    public  class UnitAddComponentSystem : AddComponentSysSystem<Unit>
    {
        protected override void AddComponent(Unit unit, Entity component)
        { 
            Type type = component.GetType();
            if (!(typeof (IUnitCache).IsAssignableFrom(type)) )
            {
                return;
            }
            unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        }
    }
    
    public class UnitGetComponentSystem : GetComponentSysSystem<Unit>
    {
        protected override void GetComponentSys(Unit unit, Type type)
        {
            if (!(typeof (IUnitCache).IsAssignableFrom(type)) )
            {
                return;
            }
            unit.GetComponent<UnitDBSaveComponent>()?.AddChange(type);
        }
    }
    
    [Invoke(TimerInvokeType.SaveChangeDBData)]
    public class UnitDBSaveComponentTimer : ATimer<UnitDBSaveComponent>
    {
        protected override void Run(UnitDBSaveComponent self)
        {
            try
            {
                if (self.IsDisposed || self.Parent == null)
                {
                   return;
                }

                if (self.Root() == null)
                {
                    return;
                }

                self?.SaveChange();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
            }
        }
    }

    public static class UnitDBSaveComponentSystem
    {
        public static void AddChange(this UnitDBSaveComponent self, Type type)
        {
            self.EntityChangeTypeSet.Add(type);
        }

        public static void SaveChange(this UnitDBSaveComponent self)
        {
            if (self.EntityChangeTypeSet.Count <= 0)
            {
                return;
            }

            Unit unit = self.GetParent<Unit>();
            
            //这里需要研究下
            Other2UnitCache_AddOrUpdateUnit message = Other2UnitCache_AddOrUpdateUnit.Create();
            message.UnitId = unit.Id;
            message.EntityTypes = new List<string>();
            message.EntityBytes = new List<byte[]>();
            message.EntityTypes.Add(unit.GetType().FullName);
            message.EntityBytes.Add(MongoHelper.Serialize(unit));

            foreach (Type type in self.EntityChangeTypeSet)
            {
                Entity entity = unit.GetComponent(type);
                //这里获的entity 是Unit身上的组件
                if (entity == null || entity.IsDisposed)
                {
                    continue;
                }
                Log.Info("开始保存变化部分的Entity数据 : " + type.FullName );
                message.EntityTypes.Add(type.FullName);
                message.EntityBytes.Add(MongoHelper.Serialize(entity));
            }
            self.EntityChangeTypeSet.Clear();
            //发给游戏缓存服进行缓存 将游戏数据写入游戏数据库
            self.Root().GetComponent<MessageSender>().Call(StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Zone()).ActorId, message).Coroutine();
        }
    }
}