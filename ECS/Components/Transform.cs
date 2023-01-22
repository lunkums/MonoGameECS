using ECS.Core;
using Microsoft.Xna.Framework;

namespace ECS.Components
{
    public struct Transform : IComponent
    {
        public Vector2 Position;
        public float Rotation;
        public Vector2 Scale;
    }
}