namespace ECS
{
    public class SpawnSystem : System, IUpdateSystem
    {
        public override ComponentMask ComponentMask => ComponentMask.Transform;

        public void Update(float deltaTime)
        {
            foreach (Entity entity in Entities)
            {
                ref Transform transform = ref entity.GetComponentReference<Transform>();

                // Respawn before the top of the window if it falls past the bottom
                transform.Position.Y = transform.Position.Y % Game1.WindowHeight
                    - ((int)transform.Position.Y / Game1.WindowHeight * 128);
            }
        }
    }
}
