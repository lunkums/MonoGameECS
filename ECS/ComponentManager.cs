using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace ECS
{
    public class ComponentManager
    {
        private Dictionary<Type, IComponentArray> componentArrays = new();

        public static ComponentMask GetMask<T>() where T : IComponent
        {
            return Enum.Parse<ComponentMask>(typeof(T).Name);
        }

        public void RegisterComponent<T>() where T : IComponent
        {
            Debug.Assert(!componentArrays.ContainsKey(typeof(T)), "Registering component type more than once.");

            componentArrays.Add(typeof(T), new ComponentArray<T>());
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
