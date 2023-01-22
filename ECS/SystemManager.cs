using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ECS
{
    public class SystemManager
    {
        private Dictionary<Type, ComponentMask> componentMasks = new();
        private Dictionary<Type, System> systems = new();

        public T RegisterSystem<T>() where T : System, new()
        {
            Debug.Assert(!systems.ContainsKey(typeof(T)), "Registering system more than once.");

            T system = new();
            systems.Add(typeof(T), system);
            return system;
        }

        public void SetSignature<T>(ComponentMask componentMask) where T : System
        {
            Debug.Assert(systems.ContainsKey(typeof(T)), "System used before registered.");

            componentMasks.Add(typeof(T), componentMask);
        }


        public void EntityDestroyed(Entity entity)
        {
            foreach (System system in systems.Values)
            {
                system.Remove(entity);
            }
        }

        public void EntitySignatureChanged(Entity entity, ComponentMask entityComponentMask)
        {
            foreach (KeyValuePair<Type, System> pair in systems)
            {
                Type type = pair.Key;
                System system = pair.Value;
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
