namespace ECS.Core
{
    public struct Rule : IComponent
    {
        public delegate void Delegate(float deltaTime);
        public Delegate Behaviour;
    }
}
