using ECS.Components;
using ECS.Core;
using Microsoft.Xna.Framework.Graphics;

namespace ECS.Systems
{
    public class SpriteSystem : ECSystem, IRenderSystem
    {
        public override ComponentMask ComponentMask => ComponentMask.Transform | ComponentMask.Sprite;

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (Entity entity in Entities)
            {
                ref Sprite sprite = ref entity.GetComponent<Sprite>();
                ref Transform transform = ref entity.GetComponent<Transform>();

                spriteBatch.Draw(
                    sprite.Texture,
                    transform.Position,
                    sprite.SourceRectangle,
                    sprite.Color,
                    transform.Rotation,
                    sprite.Origin,
                    transform.Scale,
                    sprite.Effects,
                    sprite.LayerDepth);
            }
        }
    }
}
