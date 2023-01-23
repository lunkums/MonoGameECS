using ECS.Components;
using ECS.Core;

namespace ECS
{
    public abstract partial class Agent : Entity
    {
        public Agent()
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
