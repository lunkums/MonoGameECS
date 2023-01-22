using ECS.Core;
using Microsoft.Xna.Framework;

namespace ECS.Components
{
    public class RigidBody : Component<RigidBodyData>
    {
        public Vector2 Acceleration
        {
            get => ComponentReference.Acceleration;
            set => ComponentReference.Acceleration = value;
        }

        public float AngularVelocity
        {
            get => ComponentReference.AngularVelocity;
            set => ComponentReference.AngularVelocity = value;
        }

        public Vector2 Gravity
        {
            get => ComponentReference.Gravity;
            set => ComponentReference.Gravity = value;
        }

        public Vector2 Velocity
        {
            get => ComponentReference.Velocity;
            set => ComponentReference.Velocity = value;
        }
    }
}
