namespace ET.Server
{
    [EntitySystemOf(typeof(UnitCache))]
    [FriendOf(typeof(UnitCache))]
    public static partial class UnitCacheSystem
    {
        [EntitySystem]
        private static void Awake(this UnitCache self)
        {

        }
        [EntitySystem]
        private static void Destroy(this UnitCache self)
        {
            foreach (Entity entity in self.CacheCompoenntsDictionary.Values)
            {
                entity.Dispose();
            }
            self.CacheCompoenntsDictionary.Clear();
            self.key = null;
        }

        public static void AddOrUpdate(this UnitCache self, Entity entity)
        {
            if (entity == null)
            {
                return;
            }

            if (self.CacheCompoenntsDictionary.TryGetValue(entity.Id, out EntityRef<Entity> oldEntityRef))
            {
                Entity ent = oldEntityRef;
                if (entity != ent)
                {
                    ent.Dispose();
                }

                self.CacheCompoenntsDictionary.Remove(entity.Id);
            }
            
            self.CacheCompoenntsDictionary.Add(entity.Id, entity);
        }

        public static async ETTask<Entity> Get(this UnitCache self, long unitId)
        {
            EntityRef<Entity> entity = null;
            if (!self.CacheCompoenntsDictionary.TryGetValue(unitId, out entity))
            {
                Log.Warning(">>>>>>>>get entity key:" + self.key);
                Entity ent = entity;
                ent = await self.Root().GetComponent<DBManagerComponent>().GetZoneDB(self.Zone()).Query<Entity>(unitId, self.key);

                if (ent != null)
                {
                    self.AddOrUpdate(ent);
                }
                return ent;
            }
            return null;
        }
        
        
        public static void Delete(this UnitCache self, long id)
        {
            if (self.CacheCompoenntsDictionary.TryGetValue(id, out EntityRef<Entity> entityRef))
            {
                Entity entity = entityRef;
                entity.Dispose();
                self.CacheCompoenntsDictionary.Remove(id);
            }
        }
    }
}