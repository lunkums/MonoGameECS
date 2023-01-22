﻿using Microsoft.Xna.Framework;

namespace ECS
{
    public struct RigidBody : IComponent
    {
        public Vector2 Acceleration;
        public float AngularVelocity;
        public Vector2 Gravity;
        public Vector2 Velocity;
    }
}