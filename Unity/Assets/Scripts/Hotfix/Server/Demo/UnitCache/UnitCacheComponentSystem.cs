using System;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.UnitCacheComponent))]
    [EntitySystemOf(typeof(UnitCacheComponent))]
    // [FriendOf(typeof(UnitCache))]
    public static partial class UnitCacheComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ET.Server.UnitCacheComponent self)
        {
            self.UnitCacheKeyList.Clear();
            //遍历游戏中所有类型
            foreach (Type type in CodeTypes.Instance.GetTypes().Values)
            {
                //如果有实现IUnitCache接口 将type存入list中
                if (type != typeof(IUnitCache) && typeof(IUnitCache).IsAssignableFrom(type))
                {
                    self.UnitCacheKeyList.Add(type.FullName);
                }
            }

            foreach (string key in self.UnitCacheKeyList)
            {
                UnitCache unitCache = self.AddChild<UnitCache>();
                unitCache.key = key;
                self.UnitCaches.Add(key, unitCache);
            }
        }
        [EntitySystem]
        private static void Destroy(this ET.Server.UnitCacheComponent self)
        {
            Entity entity;
            foreach (var unitCache in self.UnitCaches.Values)
            {
                entity = unitCache;
                entity?.Dispose();
            }
            self.UnitCaches.Clear();
        }

        public static async ETTask<Entity> Get(this UnitCacheComponent self, long unitId, string key)
        {
            UnitCache unitEnt = null;
            if (!self.UnitCaches.TryGetValue(key, out EntityRef<UnitCache> unitCacheRef))
            {
                unitEnt = self.AddChild<UnitCache>();
                unitEnt.key = key;
                self.UnitCaches.Add(key, unitEnt);
            }
            else
            {
                unitEnt = unitCacheRef;
            }
            
            return await unitEnt.Get(unitId);
        }
        
        public static async ETTask AddOrUpdate(this UnitCacheComponent self, long id, ListComponent<Entity> entityList)
        {
            using (ListComponent<Entity> list = ListComponent<Entity>.Create())
            {
                foreach (Entity entity in entityList)
                {
                    string key = entity.GetType().FullName;
                    UnitCache unitCache = null;
                    if (!self.UnitCaches.TryGetValue(key, out EntityRef<UnitCache> unitCacheRef))
                    {
                        unitCache = self.AddChild<UnitCache>();
                        unitCache.key = key;
                        self.UnitCaches.Add(key, unitCache);
                    }
                    else
                    {
                        unitCache = unitCacheRef;
                    }

                    unitCache.AddOrUpdate(entity);
                    list.Add(entity);
                }
                if (list.Count > 0)
                {
                    await self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Save(id, list);
                }
            }
        }

        public static void Delete(this UnitCacheComponent self, long unitId)
        {
            foreach (UnitCache cache in self.UnitCaches.Values)
            {
                cache.Delete(unitId);
            }
        }
    }
}
