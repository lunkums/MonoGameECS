using Microsoft.Xna.Framework.Graphics;

namespace ECS.Core
{
    public interface IRenderSystem : ISystem
    {
        void Render(SpriteBatch spriteBatch);
    }
}
