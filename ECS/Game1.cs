using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ECS
{
    public class Game1 : Game
    {
        public const int WindowWidth = 1024;
        public const int WindowHeight = 1024;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        private Coordinator coordinator;

        private FrameCounter counter = new();
        private SpriteFont fpsFont;

        public Game1()
        {
            _graphics = new(this)
            {
                PreferredBackBufferWidth = WindowWidth,
                PreferredBackBufferHeight = WindowHeight,
                
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            coordinator = Coordinator.Instance;

            coordinator.RegisterComponent<Transform>();
            coordinator.RegisterComponent<Sprite>();
            coordinator.RegisterComponent<RigidBody>();

            coordinator.RegisterSystem<SpriteSystem>();
            coordinator.RegisterSystem<PhysicsSystem>();
            coordinator.RegisterSystem<SpawnSystem>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new(GraphicsDevice);

            fpsFont = Content.Load<SpriteFont>("text");
            Texture2D texture = Content.Load<Texture2D>("happyface");

            Random random = new();

            for (int i = 0; i < EntityManager.MaxEntities; i++)
            {

                Entity entity = coordinator.CreateEntity();

                entity.AddComponent<Sprite>(new()
                {
                    Texture = texture,
                    SourceRectangle = new(0, 0, texture.Width, texture.Height),
                    Color = new(random.NextSingle(), random.NextSingle(), random.NextSingle()),
                    Origin = Vector2.Zero,
                    Effects = SpriteEffects.None,
                    LayerDepth = 0
                });

                entity.AddComponent<Transform>(new()
                {
                    Position = new(random.NextSingle() * WindowWidth, WindowHeight),
                    Rotation = 0,
                    Scale = new(0.125f, 0.125f)
                });

                entity.AddComponent<RigidBody>(new()
                {
                    Acceleration = Vector2.Zero,
                    Gravity = new Vector2(0, random.NextSingle() * 981f),
                    Velocity = Vector2.Zero
                });
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            coordinator.Update(deltaTime);
            counter.Update(deltaTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullNone);

            coordinator.Render(spriteBatch);
            // Draw FPS
            spriteBatch.DrawString(fpsFont, counter.CurrentFramesPerSecond.ToString(), new Vector2(1, 1), Color.Black,
                0f, Vector2.Zero, 2f, SpriteEffects.None, 0.5f);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}