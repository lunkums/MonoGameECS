using ECS.Components;
using ECS.Core;
using ECS.Systems;
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

        private Profiler updateCounter = new();
        private Profiler drawCounter = new();

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

        protected override void EndRun()
        {
            updateCounter.WriteToLog("update");
            drawCounter.WriteToLog("draw");
        }

        protected override void Initialize()
        {
            coordinator = Coordinator.Instance;

            coordinator.RegisterComponent<TransformData>(ComponentMask.Transform);
            coordinator.RegisterComponent<SpriteData>(ComponentMask.Sprite);
            coordinator.RegisterComponent<RigidBodyData>(ComponentMask.RigidBody);

            coordinator.RegisterSystem<SpriteSystem>();
            coordinator.RegisterSystem<PhysicsSystem>();
            coordinator.RegisterSystem<SpawnSystem>();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new(GraphicsDevice);

            Texture2D texture = Content.Load<Texture2D>("happyface");

            Random random = new();

            for (int i = 0; i < EntityManager.MaxEntities; i++)
            {

                Entity entity = Entity.Create();

                entity.AddComponent<SpriteData>(new()
                {
                    Texture = texture,
                    SourceRectangle = new(0, 0, texture.Width, texture.Height),
                    Color = new(random.NextSingle(), random.NextSingle(), random.NextSingle()),
                    Origin = Vector2.Zero,
                    Effects = SpriteEffects.None,
                    LayerDepth = 0
                });

                entity.AddComponent<TransformData>(new()
                {
                    Position = new(random.NextSingle() * WindowWidth, WindowHeight),
                    Rotation = 0,
                    Scale = new(0.125f, 0.125f)
                });

                entity.AddComponent<RigidBodyData>(new()
                {
                    Acceleration = Vector2.Zero,
                    AngularVelocity = random.NextSingle() * 30f - 15f,
                    Gravity = new Vector2(0, random.NextSingle() * 981f),
                    Velocity = Vector2.Zero
                });
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            updateCounter.Start();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            coordinator.Update(deltaTime);
            updateCounter.Stop();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            drawCounter.Start();
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.PointClamp,
                DepthStencilState.None, RasterizerState.CullNone);

            coordinator.Render(spriteBatch);

            spriteBatch.End();
            drawCounter.Stop();

            base.Draw(gameTime);
        }
    }
}