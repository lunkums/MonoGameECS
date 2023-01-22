using Microsoft.Xna.Framework.Graphics;

namespace ECS
{
    public class SpriteSystem : System, IRenderSystem
    {
        public override ComponentMask ComponentMask => ComponentMask.Transform | ComponentMask.Sprite;

        public void Render(SpriteBatch spriteBatch)
        {
            foreach (Entity entity in Entities)
            {
                ref Sprite sprite = ref entity.GetComponentReference<Sprite>();
                ref Transform transform = ref entity.GetComponentReference<Transform>();

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
