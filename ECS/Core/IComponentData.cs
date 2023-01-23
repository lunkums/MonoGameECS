using System;

namespace ECS.Core
{
    public interface IComponentData
    {
    }

    [Flags]
    public enum ComponentMask
    {
        None = 0,
        Transform = 1,
        RigidBody = 2,
        Sprite = 4,
        Rule = 8
    }
}