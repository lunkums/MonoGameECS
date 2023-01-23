using ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace ECS
{
    public class Player : Actor
    {
        public override void Update(float deltaTime)
        {
            RigidBody rigidBody = GetComponent<RigidBody>();
            Vector2 direction = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                direction += -Vector2.UnitY;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                direction += -Vector2.UnitX;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                direction += Vector2.UnitY;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                direction += Vector2.UnitX;
            }

            if (Math.Abs(direction.LengthSquared()) > float.Epsilon)
            {
                direction.Normalize();
            }

            rigidBody.Velocity = direction * 250;
        }
    }
}
