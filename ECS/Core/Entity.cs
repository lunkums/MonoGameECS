using System;

namespace ECS.Core
{
    public class Entity
    {
        public uint Id { get; init; }

        public static Entity Create()
        {
            return Coordinator.Instance.CreateEntity();
        }

        public void AddComponent<T>(T component) where T : IComponentData
        {
            Coordinator.Instance.AddComponent(this, component);
        }

        public ref T GetComponentReference<T>() where T : IComponentData
        {
            return ref Coordinator.Instance.GetComponent<T>(this);
        }

        public T GetComponent<T>() where T : IComponentWrapper, new()
        {
            T t = new()
            {
                Owner = this
            };
            return t;
        }

        public void SetComponent<T>(T component) where T : IComponentData
        {
            Coordinator.Instance.SetComponent<T>(this, component);
        }

        public void DestroySelf()
        {
            Coordinator.Instance.DestroyEntity(this);
        }
    }
}