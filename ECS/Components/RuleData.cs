using ECS.Core;

namespace ECS.Components
{
    public struct RuleData : IComponentData
    {
        public UpdateRule Rule;
        public delegate void UpdateRule(Entity entity, float deltaTime);
    }
}
