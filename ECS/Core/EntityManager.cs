using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ECS.Core
{
    public class EntityManager
    {
        // Release mode allows for higher number of max entities
        // 32768 is a stable max (Release) on an RTX 3070Ti + AMD Ryzen 5 2600X Six-Core Processor @ 3.60 GHz
        public static readonly ushort MaxEntities = 16384;

        private Queue<uint> availableIds = new();
        private ComponentMask[] componentMasks = new ComponentMask[MaxEntities];
        private uint livingEntityCount = 0;

        public EntityManager()
        {
            for (uint id = 0; id < MaxEntities; ++id)
            {
                availableIds.Enqueue(id);
            }
        }

        public void Destroy(Entity entity)
        {
            uint id = entity.Id;
            Debug.Assert(id < MaxEntities, "Entity out of range.");

            componentMasks[id] = ComponentMask.None;
            availableIds.Enqueue(id);
            --livingEntityCount;
        }

        public void SetComponentMask(Entity entity, ComponentMask componentMask)
        {
            uint id = entity.Id;
            Debug.Assert(id < MaxEntities, "Entity out of range.");

            componentMasks[id] = componentMask;
        }

        public ComponentMask GetComponentMask(Entity entity)
        {
            uint id = entity.Id;
            Debug.Assert(id < MaxEntities, "Entity out of range.");

            return componentMasks[id];
        }

        internal void RegisterEntity(Entity entity)
        {
            Debug.Assert(livingEntityCount < MaxEntities, "Too many entities in existence.");
            Debug.Assert(entity.Id == GetNextAvailableId(), "Unregistered entity in existence.");

            availableIds.Dequeue();
            ++livingEntityCount;
        }

        internal uint GetNextAvailableId()
        {
            return availableIds.Peek();
        }
    }
}