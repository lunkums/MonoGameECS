using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ECS.Core
{
    public sealed class Coordinator
    {
        private static readonly Coordinator instance = new();

        private ComponentManager componentManager;
        private EntityManager entityManager;
        private SystemManager systemManager;

        private List<IUpdateSystem> updateSystems;
        private List<IRenderSystem> renderSystems;

        // The event queue is necessary to modify component arrays, and the global list of entities, without modifying
        // enumerators during iteration
        private Queue<Action> eventQueue;

        static Coordinator() { }

        private Coordinator()
        {
            // Managers
            componentManager = new();
            entityManager = new();
            systemManager = new();

            // System collections
            updateSystems = new();
            renderSystems = new();

            eventQueue = new();
        }

        public static Coordinator Instance => instance;

        // Entity methods

        public Entity CreateEntity()
        {
            return entityManager.Create();
        }

        public void DestroyEntity(Entity entity)
        {
            entityManager.Destroy(entity);

            eventQueue.Enqueue(() =>
            {
                componentManager.EntityDestroyed(entity);
                systemManager.EntityDestroyed(entity);
            });
        }

        // Component methods

        public void RegisterComponent<T>() where T : IComponent
        {
            componentManager.RegisterComponent<T>();
        }

        public void AddComponent<T>(Entity entity, T component) where T : IComponent
        {
            eventQueue.Enqueue(() =>
            {
                componentManager.Add(entity, component);

                ComponentMask signature = entityManager.GetComponentMask(entity);
                signature |= ComponentManager.GetMask<T>();
                entityManager.SetComponentMask(entity, signature);

                systemManager.EntityMaskChanged(entity, signature);
            });
        }

        public void RemoveComponent<T>(Entity entity) where T : IComponent
        {
            eventQueue.Enqueue(() =>
            {
                componentManager.Remove<T>(entity);

                ComponentMask signature = entityManager.GetComponentMask(entity);
                signature &= ~ComponentManager.GetMask<T>();
                entityManager.SetComponentMask(entity, signature);

                systemManager.EntityMaskChanged(entity, signature);
            });
        }

        public ref T GetComponent<T>(Entity entity) where T : IComponent
        {
            return ref componentManager.Get<T>(entity);
        }

        // System methods

        public void RegisterSystem<T>() where T : ECSystem, new()
        {
            T system = systemManager.RegisterSystem<T>();
            systemManager.SetComponentMask<T>(system.ComponentMask);

            if (system is IUpdateSystem updateSystem)
            {
                updateSystems.Add(updateSystem);
            }

            if (system is IRenderSystem renderSystem)
            {
                renderSystems.Add(renderSystem);
            }
        }

        // Iteration methods

        public void Update(float deltaTime)
        {
            updateSystems.ForEach(us => us.Update(deltaTime));
            ProcessEventQueue();
        }

        public void Render(SpriteBatch spriteBatch)
        {
            renderSystems.ForEach(rs => rs.Render(spriteBatch));
            ProcessEventQueue();
        }

        private void ProcessEventQueue()
        {
            while (eventQueue.TryDequeue(out Action action))
            {
                action();
            }
        }
    }
}