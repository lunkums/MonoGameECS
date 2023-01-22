using System.Collections.Generic;
using System.Diagnostics;

namespace ECS.Core
{
    public class ComponentArray<T> : IComponentArray where T : IComponentData
    {
        private T[] componentArray;
        private Dictionary<uint, int> entityToIndexMap;
        private Dictionary<int, uint> indexToEntityMap;
        private int size;

        public ComponentArray()
        {
            componentArray = new T[EntityManager.MaxEntities];
            entityToIndexMap = new();
            indexToEntityMap = new();
            size = 0;
        }

        public void Insert(Entity entity, T component)
        {
            uint id = entity.Id;
            Debug.Assert(!entityToIndexMap.ContainsKey(id), "Component added to same entity more than once.");

            // Put new entry at end
            int newIndex = size;
            entityToIndexMap[id] = newIndex;
            indexToEntityMap[newIndex] = id;
            componentArray[newIndex] = component;
            ++size;
        }

        public void Remove(Entity entity)
        {
            uint id = entity.Id;
            Debug.Assert(entityToIndexMap.ContainsKey(id), "Removing non-existent component.");

            // Copy element at end into deleted element's place to maintain density
            int indexOfRemovedEntity = entityToIndexMap[id];
            int indexOfLastElement = size - 1;
            componentArray[indexOfRemovedEntity] = componentArray[indexOfLastElement];

            // Update map to point to moved spot
            uint entityOfLastElement = indexToEntityMap[indexOfLastElement];
            entityToIndexMap[entityOfLastElement] = indexOfRemovedEntity;
            indexToEntityMap[indexOfRemovedEntity] = entityOfLastElement;

            entityToIndexMap.Remove(id);
            indexToEntityMap.Remove(indexOfLastElement);

            --size;
        }

        public ref T Get(Entity entity)
        {
            uint id = entity.Id;
            Debug.Assert(entityToIndexMap.ContainsKey(id), "Retrieving non-existent component.");

            return ref componentArray[entityToIndexMap[id]];
        }

        public void Set(Entity entity, T component)
        {
            uint id = entity.Id;
            // Could provide a more lenient interface by inserting if not present in the component array
            Debug.Assert(entityToIndexMap.ContainsKey(id), "Setting non-existent component.");

            componentArray[id] = component;
        }

        public void EntityDestroyed(Entity entity)
        {
            if (entityToIndexMap.ContainsKey(entity.Id))
            {
                Remove(entity);
            }
        }
    }
}