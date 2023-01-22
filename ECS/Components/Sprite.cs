using ECS.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ECS.Components
{
    public struct Sprite : IComponent
    {
        public Texture2D Texture;
        public Rectangle? SourceRectangle;
        public Color Color;
        public Vector2 Origin;
        public SpriteEffects Effects;
        public float LayerDepth;
    }
}