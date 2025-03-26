# MultiStateDemo

## General
The purpose of this project is to demonstrate my ability to create a prototype 3D action game featuring a player with multiple states.

The player has three power states:
* Normal: Fast move speed, slow shoot speed
* Empowered: Slow move speed, fast shoot speed
* Ghost: Medium move speed, inability to shoot, can clip through obstacles

Open `Assets/Scenes/Main` to play the game.

## Controls
WASD to move

Space to jump

Left mouse click to shoot

### Debugging
1 to change to Normal state

2 to change to Ghost state

3 to change to Empowered state

or use the on screen debugger to change your current state

## Architecture
### Finite State Machine
As the primary requirement for the project was to create a player with multiple states, I chose to implement this using Finite State Machines. This allows the states' behaviours to have a common structure, while still be isolated from one another. I chose to build these states off of `ScriptableObjects` so that designers could easily add and remove states, and the states' data can be tuned easily in the editor.

### Reactive Values
To improve performance, player facing values such as health and score are wrapped in a `Reactive<T>` wrapper. This object allows other objects to subscribe to value change events, enabling the pub-sub pattern which is more performant than polling.

### Pooling
To improve performance, projectiles are stored in an `ObjectPool`. After a projectile has collided with another object, or has reached its maximum lifespan, it will return to the pool. This is an optimization, trading some memory for increased performance and reduced garbage collection.

## Balancing
Use the prefabs and scriptable objects within the `Assets/Content` folder to balance the game:
* `Assets/Content/Enemies`: Control score value and damage to player
* `Assets/Content/Projectiles`: Control projectile speed and lifespan
* `Assets/Content/Player States`: Control movement speed, attack input, attack speed, and collision
