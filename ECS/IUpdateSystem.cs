namespace ECS
{
    public interface IUpdateSystem : ISystem
    {
        void Update(float deltaTime);
    }
}
