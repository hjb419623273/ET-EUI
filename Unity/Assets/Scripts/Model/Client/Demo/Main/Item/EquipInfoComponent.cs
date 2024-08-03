using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Item))]
    public class EquipInfoComponent : Entity,IAwake,IDestroy
    {
        public bool IsInited = false;
        
        //装备评分
        public int Score = 0;
        
        //装备词条列表
        public List<EntityRef<AttributeEntry>> EntryList = new List<EntityRef<AttributeEntry>>();
    }
}