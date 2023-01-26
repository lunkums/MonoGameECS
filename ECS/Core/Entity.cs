namespace ECS.Core
{
    public class Entity
    {
        public Entity()
        {
            Id = Coordinator.Instance.GetNextAvailableId();
            Coordinator.Instance.RegisterEntity(this);
        }

        public uint Id { get; }

        public void AddComponent<T>(T component) where T : IComponent
        {
            Coordinator.Instance.AddComponent(this, component);
        }

        public ref T GetComponent<T>() where T : IComponent
        {
            return ref Coordinator.Instance.GetComponent<T>(this);
        }
    }
}