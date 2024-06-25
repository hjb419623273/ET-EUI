using System;
using System.Collections.Generic;

namespace ET.Server
{
    public static class UnitCacheHelper
    {
        /// <summary>
        /// 保存或者更新玩家缓存
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static async ETTask AddOrUpdateUnitCache<T>(this T self) where T : Entity, IUnitCache
        {
            Other2UnitCache_AddOrUpdateUnit message = Other2UnitCache_AddOrUpdateUnit.Create();
            message.UnitId = self.Id;
            message.EntityTypes = new List<string>();
            message.EntityBytes = new List<byte[]>();
            message.EntityTypes.Add(typeof(T).FullName);
            message.EntityBytes.Add(MongoHelper.Serialize(self));
            await self.Root().GetComponent<MessageSender>().Call(StartSceneConfigCategory.Instance.GetUnitCacheConfig(self.Zone()).ActorId, message);
        }
        /// <summary>
        /// 获取玩家缓存
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static async ETTask<Unit> GetUnitCache(Scene scene, long unitId)
        {
            Scene root = scene.Root();
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetUnitCacheConfig(scene.Zone());
            Other2UnitCache_GetUnit message = Other2UnitCache_GetUnit.Create();
            message.UnitId = unitId;
            UnitCache2Other_GetUnit queryUnit = (UnitCache2Other_GetUnit)await root.GetComponent<MessageSender>().Call(startSceneConfig.ActorId, message);
            if (queryUnit.Error != ErrorCode.ERR_Success ||  queryUnit.EntityList == null || queryUnit.EntityList.Count <= 0)
            {
                return null;
            }
            int indexOf = queryUnit.ComponentNameList.IndexOf(typeof(Unit).FullName);
            Unit unit = queryUnit.EntityList[indexOf] as Unit;
            if (unit == null)
            {
                return null;
            }
            
            //unit挂载在scene下
            scene.GetComponent<UnitComponent>().AddChild(unit);
            foreach (Entity entity in queryUnit.EntityList)
            {
                if (entity == null || entity is Unit)
                {
                    continue;
                }
                
                //其他的实体数据挂载在unit上
                unit.AddComponent(entity);
            }

            return unit;
        }

        /// <summary>
        /// 获取玩家组件缓存
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="unitId"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static async ETTask<T> GetUnitComponentCache<T>(Scene scene, long unitId) where T : Entity, IUnitCache
        {
            Scene root = scene.Root();
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetUnitCacheConfig(scene.Zone());

            Other2UnitCache_GetUnit message = Other2UnitCache_GetUnit.Create();
            message.UnitId = unitId;
            message.ComponentNameList = new List<string>();
            message.ComponentNameList.Add(typeof (T).Name);
            
            ActorId actorId = startSceneConfig.ActorId;
            UnitCache2Other_GetUnit queryUnit = (UnitCache2Other_GetUnit)await root.GetComponent<MessageSender>().Call(actorId, message);
            if (queryUnit.Error == ErrorCode.ERR_Success && queryUnit.EntityList !=null && queryUnit.EntityList.Count > 0)
            {
                return queryUnit.EntityList[0] as T;
            }
            return null;
        }

        /// <summary>
        /// 删除玩家缓存
        /// </summary>
        /// <param name="scene"></param>
        /// <param name="unitId"></param>
        public static async ETTask DeleteUnitCache(Scene scene, long unitId)
        {
            Scene root = scene.Root();
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetUnitCacheConfig(scene.Zone());
            Other2UnitCache_DeleteUnit message = Other2UnitCache_DeleteUnit.Create();
            await root.GetComponent<MessageSender>().Call(startSceneConfig.ActorId, message);
        }
        
        /// <summary>
        /// 保存unit及unit身上组件到缓存服以及数据库中 
        /// </summary>
        /// <param name="unit"></param>
        public static void AddOrUpdateUnitAllCache(Unit unit)
        {
            Other2UnitCache_AddOrUpdateUnit message = Other2UnitCache_AddOrUpdateUnit.Create();
            message.UnitId = unit.Id;
            message.EntityTypes = new List<string>();
            message.EntityBytes = new List<byte[]>();
            message.EntityTypes.Add(unit.GetType().FullName);
            message.EntityBytes.Add(MongoHelper.Serialize(unit));
            foreach ((long id, Entity entity) in unit.Components)
            {
                //实现IUnitCache接口 实体才会储存
                Type key = entity.GetType();
                if (!typeof(IUnitCache).IsAssignableFrom(key))
                {
                    continue;
                }
                message.EntityTypes.Add(entity.GetType().FullName);
                message.EntityBytes.Add(MongoHelper.Serialize(entity));
            }
            // unit.Root().GetComponent<MessageSender>().Call(StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Id).ActorId, message).Coroutine();
            Scene root = unit.Root();
            StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unit.Zone());
            root.GetComponent<MessageSender>().Call(startSceneConfig.ActorId, message).Coroutine();
        }
    }
}