using System;
using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Unit))]
    public class UnitDBSaveComponent : Entity, IAwake, IDestroy
    {
        //HashSet中值是唯一的不能产生重复
        public HashSet<Type> EntityChangeTypeSet { get; } = new HashSet<Type>();
        
        public long Timer;
    }
}