using System;

namespace ET
{
    public interface IAddComponentSys
    {
    }
	
    public interface IAddComponentSysSystem: ISystemType
    {
        void Run(Entity o, Entity component);
    }

    [EntitySystem]
    public abstract class AddComponentSysSystem<T> :SystemObject, IAddComponentSysSystem where T: Entity, IAddComponentSys
    {
        void IAddComponentSysSystem.Run(Entity o, Entity component)
        {
            this.AddComponent((T)o, component);
        }

        Type ISystemType.SystemType()
        {
            return typeof(IAddComponentSysSystem);
        }

        int ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        Type ISystemType.Type()
        {
            return typeof(T);
        }

        protected abstract void AddComponent(T self, Entity component);
    }
}