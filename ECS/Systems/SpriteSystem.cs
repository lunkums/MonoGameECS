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
                ref SpriteData sprite = ref entity.GetComponentReference<SpriteData>();
                ref TransformData transform = ref entity.GetComponentReference<TransformData>();

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
