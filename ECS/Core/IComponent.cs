using System;

namespace ECS.Core
{
    public interface IComponent
    {
    }

    [Flags]
    public enum ComponentMask
    {
        None = 0,
        Transform = 1,
        RigidBody = 2,
        Sprite = 4,
        AnimationController = 8
    }
}