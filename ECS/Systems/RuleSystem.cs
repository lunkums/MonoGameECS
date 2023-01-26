using ECS.Core;

namespace ECS.Systems
{
    public class RuleSystem : ECSystem, IUpdateSystem
    {
        public override ComponentMask ComponentMask => ComponentMask.Rule;

        public void Update(float deltaTime)
        {
            foreach (Entity entity in Entities)
            {
                ref Rule rule = ref entity.GetComponent<Rule>();
                rule.Behaviour(deltaTime);
            }
        }
    }
}
