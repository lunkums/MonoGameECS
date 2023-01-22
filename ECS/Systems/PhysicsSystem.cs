using ECS.Components;
using ECS.Core;
using Microsoft.Xna.Framework;

namespace ECS.Systems
{
    public class PhysicsSystem : ECSystem, IUpdateSystem
    {
        public static readonly Vector2 TerminalVelocity = new(Game1.WindowWidth, Game1.WindowHeight);

        public override ComponentMask ComponentMask => ComponentMask.Transform | ComponentMask.RigidBody;

        public void Update(float deltaTime)
        {
            foreach (Entity entity in Entities)
            {
                ref RigidBodyData rigidBody = ref entity.GetComponentReference<RigidBodyData>();
                ref TransformData transform = ref entity.GetComponentReference<TransformData>();

                rigidBody.Acceleration += rigidBody.Gravity * deltaTime;
                rigidBody.Velocity += rigidBody.Acceleration * deltaTime;
                rigidBody.Velocity = Vector2.Min(rigidBody.Velocity, TerminalVelocity);
                transform.Position += rigidBody.Velocity * deltaTime;
                transform.Rotation += rigidBody.AngularVelocity * deltaTime;
            }
        }
    }
}
