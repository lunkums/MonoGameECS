namespace ECS.Core
{
    public abstract class Actor : Entity
    {
        public Actor()
        {
            AddComponent<Rule>(new() { Behaviour = Update });
            Coordinator.Instance.InitializeActor(this);
        }

        public abstract void Initialize();

        public abstract void Update(float deltaTime);
    }
}