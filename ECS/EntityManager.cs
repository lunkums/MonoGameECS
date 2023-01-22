using System.Collections.Generic;
using System.Diagnostics;

namespace ECS
{
    public class EntityManager
    {
        public static readonly ushort MaxEntities = ushort.MaxValue / 2;

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

        public Entity Create()
        {
            Debug.Assert(livingEntityCount < MaxEntities, "Too many entities in existence.");

            Entity entity = new()
            {
                Id = availableIds.Dequeue()
            };
            ++livingEntityCount;

            return entity;
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
    }
}