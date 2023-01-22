using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace ECS.Core
{
    public class ComponentManager
    {
        private Dictionary<Type, IComponentArray> componentArrays = new();
        private Dictionary<Type, ComponentMask> componentMasks = new();

        public ComponentMask GetMask<T>() where T : IComponentData
        {
            Debug.Assert(componentArrays.ContainsKey(typeof(T)), "Component mask not registered before use.");

            return componentMasks[typeof(T)];
        }

        public void RegisterComponent<T>(ComponentMask componentMask) where T : IComponentData
        {
            Debug.Assert(!componentArrays.ContainsKey(typeof(T)), "Registering component type more than once.");

            componentArrays.Add(typeof(T), new ComponentArray<T>());
            componentMasks[typeof(T)] = componentMask;
        }

        public void Add<T>(Entity entity, T component) where T : IComponentData
        {
            GetComponentArray<T>().Insert(entity, component);
        }

        public void Remove<T>(Entity entity) where T : IComponentData
        {
            GetComponentArray<T>().Remove(entity);
        }

        public ref T Get<T>(Entity entity) where T : IComponentData
        {
            return ref GetComponentArray<T>().Get(entity);
        }

        public void Set<T>(Entity entity, T component) where T : IComponentData
        {
            GetComponentArray<T>().Set(entity, component);
        }

        public void EntityDestroyed(Entity entity)
        {
            foreach (var componentArray in componentArrays.Values)
            {
                componentArray.EntityDestroyed(entity);
            }
        }

        private ComponentArray<T> GetComponentArray<T>() where T : IComponentData
        {
            Debug.Assert(componentArrays.ContainsKey(typeof(T)), "Component not registered before use.");

            return (ComponentArray<T>)componentArrays[typeof(T)];
        }
    }
}
