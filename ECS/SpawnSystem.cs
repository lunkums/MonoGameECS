using Microsoft.Xna.Framework;
using System;

namespace ECS
{
    public class SpawnSystem : System, IUpdateSystem
    {
        private Random random = new Random();

        public override ComponentMask ComponentMask => ComponentMask.Transform | ComponentMask.RigidBody;

        public void Update(float deltaTime)
        {
            foreach (Entity entity in Entities)
            {
                ref Transform transform = ref entity.GetComponentReference<Transform>();

                if (transform.Position.Y < Game1.WindowHeight) return;

                entity.DestroySelf();

                Entity replacement = Entity.Create();

                RigidBody rigidBody = entity.GetComponent<RigidBody>();
                rigidBody.Acceleration = Vector2.Zero;
                replacement.AddComponent(rigidBody);
                replacement.AddComponent(new Transform()
                {
                    Position = new(transform.Position.X, -random.Next(128 + 2 * Game1.WindowHeight)),
                    Rotation = transform.Rotation,
                    Scale = transform.Scale
                });
                replacement.AddComponent(entity.GetComponentReference<Sprite>());
            }
        }
    }
}
