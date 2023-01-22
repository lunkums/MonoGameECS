using ECS.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS.Components
{
    public class Sprite : Component<SpriteData>
    {
        public Texture2D Texture
        {
            get => ComponentReference.Texture;
            set => ComponentReference.Texture = value;
        }

        public Rectangle? SourceRectangle
        {
            get => ComponentReference.SourceRectangle;
            set => ComponentReference.SourceRectangle = value;
        }

        public Color Color
        {
            get => ComponentReference.Color;
            set => ComponentReference.Color = value;
        }

        public Vector2 Origin
        {
            get => ComponentReference.Origin;
            set => ComponentReference.Origin = value;
        }

        public SpriteEffects Effects
        {
            get => ComponentReference.Effects;
            set => ComponentReference.Effects = value;
        }

        public float LayerDepth
        {
            get => ComponentReference.LayerDepth;
            set => ComponentReference.LayerDepth = value;
        }

    }
}
