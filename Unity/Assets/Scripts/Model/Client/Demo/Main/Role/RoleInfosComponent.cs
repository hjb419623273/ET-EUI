using System.Collections.Generic;
namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class RoleInfosComponent : Entity, IAwake,IDestroy
    {
        public List<EntityRef<RoleInfo>> RoleInfos = new List<EntityRef<RoleInfo>>();
        public long CurrentRoleId = 0;
    }
}