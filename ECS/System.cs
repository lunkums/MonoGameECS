using System.Collections.Generic;

namespace ECS
{
    public abstract class System : ISystem
    {
        private HashSet<Entity> entities = new();

        public IEnumerable<Entity> Entities => entities;

        public abstract ComponentMask ComponentMask { get; }

        public void Add(Entity entity)
        {
            entities.Add(entity);
        }

        public void Remove(Entity entity)
        {
            entities.Remove(entity);
        }
    }
}
