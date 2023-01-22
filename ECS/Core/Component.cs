namespace ECS.Core
{
    public abstract class Component<T> : IComponentWrapper where T : IComponentData
    {
        private Entity entity;

        public ref T ComponentReference => ref entity.GetComponentReference<T>();
        public Entity Owner
        {
            get => entity;
            set
            {
                if (entity != null)
                {
                    ref T componentReference = ref ComponentReference;
                    value.SetComponent(componentReference);
                }
                entity = value;
            }
        }
    }
}