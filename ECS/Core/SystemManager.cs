using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ECS.Core
{
    public class SystemManager
    {
        private Dictionary<Type, ComponentMask> componentMasks = new();
        private Dictionary<Type, ECSystem> systems = new();

        public T RegisterSystem<T>() where T : ECSystem, new()
        {
            Debug.Assert(!systems.ContainsKey(typeof(T)), "Registering system more than once.");

            T system = new();
            systems.Add(typeof(T), system);
            return system;
        }

        public void SetComponentMask<T>(ComponentMask componentMask) where T : ECSystem
        {
            Debug.Assert(systems.ContainsKey(typeof(T)), "System used before registered.");

            componentMasks.Add(typeof(T), componentMask);
        }


        public void EntityDestroyed(Entity entity)
        {
            foreach (ECSystem system in systems.Values)
            {
                system.Remove(entity);
            }
        }

        public void EntityMaskChanged(Entity entity, ComponentMask entityComponentMask)
        {
            foreach (KeyValuePair<Type, ECSystem> pair in systems)
            {
                Type type = pair.Key;
                ECSystem system = pair.Value;
                ComponentMask systemComponentMask = componentMasks[type];

                if (entityComponentMask.HasFlag(systemComponentMask))
                {
                    system.Add(entity);
                }
                else
                {
                    system.Remove(entity);
                }
            }
        }
    }
}
