using Microsoft.Xna.Framework.Graphics;

namespace ECS
{
    public interface IRenderSystem : ISystem
    {
        void Render(SpriteBatch spriteBatch);
    }
}
