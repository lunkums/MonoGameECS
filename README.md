<!-- ABOUT THE PROJECT -->
## MonoGame ECS

<p align="center">
  <img src="./Image/thumb.gif" alt="animated" />
</p>

A simple Entity Component System, adapted from [Austin Morlan's ECS](https://austinmorlan.com/posts/entity_component_system/) and powered by [MonoGame](https://www.monogame.net/).

Support up to 32768 (2^15) entities in Release mode and 16384 (2^14) in Debug mode. Tested on an RTX 3070Ti + AMD Ryzen 5 2600X Six-Core Processor @ 3.60 GHz.

## Core vs. Main Branch

The [ecs-core](https://github.com/lunkums/MonoGameECS/tree/ecs-core) branch includes a basic ECS adapted from Austin Morlan's. The [main](https://github.com/lunkums/MonoGameECS/tree/main) branch includes additional code in which components are wrapped in objects (implementing `IComponentWrapper`). These objects store their owning entity's ID so that their properties can be modified and so that these modifications will be reflected in the components stored in the ECS.

The advantage of these OOP wrappers is that it provides users a way to directly modify ECS components without worrying about handling struct references. The disadvantage of them is that they require redundant code and boilerplate. They would likely be used when defining custom entity logic (i.e. if you have a `Player` class that extends `Entity` with an `Update(float deltaTime)` method that modifies the transform under some condition).

## Usage

### Components (ecs-core)

To add a new component type using the ecs-core branch:
1. Define a new struct as follows: `public struct COMPONENT_NAME : IComponent`.
2. Register the component with the coordinator in `Game1.cs` (I recommend under the `Initialize()` method).
3. Provide a component mask when registering a component, which needs to be added to the `ComponentMask` enum (adhering to the rules of the [FlagsAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-7.0)).

### Components (main [OOP])

o add a new component type using the main (OOP) branch:
1. Define a new struct as follows: `public struct COMPONENT_NAME : IComponentData` (I recommend appending the word 'Data' to the struct name, such as TransformData, to distinguish it from the component wrapper).
2. Register the component with the coordinator in `Game1.cs` (I recommend under the `Initialize()` method).
3. Provide a component mask when registering a component, which needs to be added to the `ComponentMask` enum (adhering to the rules of the [FlagsAttribute](https://learn.microsoft.com/en-us/dotnet/api/system.flagsattribute?view=net-7.0)).
4. Define a new class inheriting `Component<T>` where T implements `IComponentData` (I recommend naming this the component name, such as Transform, since the purpose of the wrapper is to be user-friendly).
5. Wrap each member of the `IComponentData` with a getter/setter as follows (replacing `T` with the actual data type):
```
        public T FieldName
        {
            get => ComponentReference.FieldName;
            set => ComponentReference.FieldName = value;
        }
```

### Systems

To add a new system type:
1. Define a class as follows: `public class SYSTEM_NAME : ECSystem, SYSTEM_TYPE` (replacing `SYSTEM_TYPE` with `IUpdateSystem` if your system will be updated using deltaTime or `IRenderSystem` if your system will be rendered using `SpriteBatch.Draw(...)`).
2. !!! Your system **will not do anything** if you don't define it as an `IUpdateSystem` or `IRenderSystem`, but feel free to add another system type if necessary.
3. Define a component mask for the system, meaning any entity with all components specified by the mask will be affected by the system. The following example defines a component mask for a system that will affect entities with a sprite and a transform.
```
public override ComponentMask ComponentMask => ComponentMask.Transform | ComponentMask.Sprite;
```
4. Register the system with the coordinator in `Game1.cs` (I recommend under the `Initialize()` method).
