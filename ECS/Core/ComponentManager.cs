using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace ECS.Core
{
    public class ComponentManager
    {
        private Dictionary<Type, IComponentArray> componentArrays = new();
        private Dictionary<Type, ComponentMask> componentMasks = new();

        public ComponentMask GetMask<T>() where T : IComponent
        {
            Debug.Assert(componentArrays.ContainsKey(typeof(T)), "Component mask not registered before use.");

            return componentMasks[typeof(T)];
        }

        public void RegisterComponent<T>(ComponentMask componentMask) where T : IComponent
        {
            Debug.Assert(!componentArrays.ContainsKey(typeof(T)), "Registering component type more than once.");

            componentArrays.Add(typeof(T), new ComponentArray<T>());
            componentMasks.Add(typeof(T), componentMask);
        }

        public void Add<T>(Entity entity, T component) where T : IComponent
        {
            GetComponentArray<T>().Insert(entity, component);
        }

        public void Remove<T>(Entity entity) where T : IComponent
        {
            GetComponentArray<T>().Remove(entity);
        }

        public ref T Get<T>(Entity entity) where T : IComponent
        {
            return ref GetComponentArray<T>().Get(entity);
        }

        public void EntityDestroyed(Entity entity)
        {
            foreach (var componentArray in componentArrays.Values)
            {
                componentArray.EntityDestroyed(entity);
            }
        }

        private ComponentArray<T> GetComponentArray<T>() where T : IComponent
        {
            Debug.Assert(componentArrays.ContainsKey(typeof(T)), "Component not registered before use.");

            return (ComponentArray<T>)componentArrays[typeof(T)];
        }
    }
}
