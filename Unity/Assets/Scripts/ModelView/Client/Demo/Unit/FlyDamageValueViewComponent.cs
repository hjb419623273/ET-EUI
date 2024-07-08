using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class FlyDamageValueViewComponent : Entity, IAwake, IDestroy
    {
        public HashSet<GameObject> FlyingDamageSet = new HashSet<GameObject>();
    }
}