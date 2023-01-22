using ECS.Core;
using Microsoft.Xna.Framework;

namespace ECS.Components
{
    public class Transform : Component<TransformData>
    {
        public Vector2 Position
        {
            get => ComponentReference.Position;
            set => ComponentReference.Position = value;
        }

        public float Rotation
        {
            get => ComponentReference.Rotation;
            set => ComponentReference.Rotation = value;
        }

        public Vector2 Scale
        {
            get => ComponentReference.Scale;
            set => ComponentReference.Scale = value;
        }
    }
}
