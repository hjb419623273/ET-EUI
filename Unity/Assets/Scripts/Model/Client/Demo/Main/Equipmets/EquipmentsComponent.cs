using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class EquipmentsComponent : Entity,IAwake,IDestroy
    {
        public Dictionary<int, EntityRef<Item>> EquipItems = new Dictionary<int, EntityRef<Item>>();
    }
}