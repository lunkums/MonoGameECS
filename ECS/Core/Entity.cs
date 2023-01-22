namespace ECS.Core
{
    public struct Entity
    {
        public uint Id { get; init; }

        public static Entity Create()
        {
            return Coordinator.Instance.CreateEntity();
        }

        public void AddComponent<T>(T component) where T : IComponent
        {
            Coordinator.Instance.AddComponent(this, component);
        }

        public ref T GetComponentReference<T>() where T : IComponent
        {
            return ref Coordinator.Instance.GetComponent<T>(this);
        }
    }
}