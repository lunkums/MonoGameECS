using ECS.Components;

namespace ECS.Core
{
    public abstract partial class Actor : Entity
    {
        public Actor()
        {
            AddRuleComponent();
        }

        public abstract void Update(float deltaTime);

        private void AddRuleComponent()
        {
            RuleData ruleData = new()
            {
                Rule = (_, deltaTime) => Update(deltaTime)
            };
            AddComponent(ruleData);
        }
    }
}
