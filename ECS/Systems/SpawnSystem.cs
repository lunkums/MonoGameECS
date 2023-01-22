using ECS.Components;
using ECS.Core;

namespace ECS.Systems
{
    public class SpawnSystem : ECSystem, IUpdateSystem
    {
        public override ComponentMask ComponentMask => ComponentMask.Transform;

        public void Update(float deltaTime)
        {
            foreach (Entity entity in Entities)
            {
                ref TransformData transform = ref entity.GetComponentReference<TransformData>();

                // Respawn before the top of the window if it falls past the bottom
                transform.Position.Y = transform.Position.Y % Game1.WindowHeight
                    - (int)transform.Position.Y / Game1.WindowHeight * 128;
            }
        }
    }
}
