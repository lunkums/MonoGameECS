using ECS.Components;
using ECS.Core;

namespace ECS
{
    public class RuleSystem : ECSystem, IUpdateSystem
    {
        public override ComponentMask ComponentMask => ComponentMask.Rule;

        public void Update(float deltaTime)
        {
            foreach (Entity entity in Entities)
            {
                ref RuleData ruleData = ref entity.GetComponentReference<RuleData>();
                ruleData.Rule(entity, deltaTime);
            }
        }
    }
}
