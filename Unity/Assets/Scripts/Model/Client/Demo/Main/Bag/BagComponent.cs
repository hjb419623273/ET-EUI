using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class BagComponent: Entity,IAwake,IDestroy
    {
        public Dictionary<long, EntityRef<Item>> ItemsDict = new Dictionary<long, EntityRef<Item> >(); 
        public MultiMap<int, EntityRef<Item>> ItemsMap = new MultiMap<int, EntityRef<Item>>();
    }
}