using System.Collections.Generic;
using System.Diagnostics;

namespace ECS
{

    public class ComponentArray<T> : IComponentArray where T : IComponent
    {

        private T[] componentArray;
        private Dictionary<uint, int> mEntityToIndexMap;
        private Dictionary<int, uint> mIndexToEntityMap;
        private int mSize;

        public ComponentArray()
        {
            componentArray = new T[EntityManager.MaxEntities];
            mEntityToIndexMap = new();
            mIndexToEntityMap = new();
            mSize = 0;
        }
    
        public void Insert(Entity entity, T component)
        {
            uint id = entity.Id;
            Debug.Assert(!mEntityToIndexMap.ContainsKey(id), "Component added to same entity more than once.");

            // Put new entry at end
            int newIndex = mSize;
            mEntityToIndexMap[id] = newIndex;
            mIndexToEntityMap[newIndex] = id;
            componentArray[newIndex] = component;
            ++mSize;
        }

        public void Remove(Entity entity)
        {
            uint id = entity.Id;
            Debug.Assert(mEntityToIndexMap.ContainsKey(id), "Removing non-existent component.");

            // Copy element at end into deleted element's place to maintain density
            int indexOfRemovedEntity = mEntityToIndexMap[id];
            int indexOfLastElement = mSize - 1;
            componentArray[indexOfRemovedEntity] = componentArray[indexOfLastElement];

            // Update map to point to moved spot
            uint entityOfLastElement = mIndexToEntityMap[indexOfLastElement];
            mEntityToIndexMap[entityOfLastElement] = indexOfRemovedEntity;
            mIndexToEntityMap[indexOfRemovedEntity] = entityOfLastElement;

            mEntityToIndexMap.Remove(id);
            mIndexToEntityMap.Remove(indexOfLastElement);

            --mSize;
        }

        public ref T Get(Entity entity)
        {
            uint id = entity.Id;
            Debug.Assert(mEntityToIndexMap.ContainsKey(id), "Retrieving non-existent component.");

            return ref componentArray[mEntityToIndexMap[id]];
        }

        public void EntityDestroyed(Entity entity)
        {
            if (mEntityToIndexMap.ContainsKey(entity.Id))
            {
                Remove(entity);
            }
        }
    }
}