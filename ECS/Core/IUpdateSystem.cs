namespace ECS.Core
{
    public interface IUpdateSystem : ISystem
    {
        void Update(float deltaTime);
    }
}
