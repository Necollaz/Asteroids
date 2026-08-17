# Asteroids

2D clone of the original Asteroids game made as a Unity programmer test assignment.

## Unity Version

Unity 2022.3.9f

## Project Goal

The goal is to implement a 2D Asteroids-style game with custom physics, Zenject-based architecture, SignalBus, MVVM UI, object pooling, configurable gameplay settings, Firebase Analytics, and an ads adapter.

## Current Status

Implemented:
- Custom player movement with acceleration, inertia, rotation, damping, and screen wrapping
- Custom circle collision detection without Unity Physics gameplay callbacks
- Player health, damage, knockback, defeat flow, and invulnerability
- Bullet shooting with cooldown, lifetime, object pool, and enemy collision handling
- Laser shooting with limited charges, recharge, visual beam, and enemy damage
- Asteroids with large/medium/small split chain
- UFO spawning, movement toward player, damage, knockback, and destruction
- Enemy reward table by `EnemyType`
- Shared enemy access through facade/registry
- MVVM-based gameplay UI
- Keyboard and mobile input sources
- JSON/ScriptableObject gameplay settings
- Ads service abstraction
- Analytics service abstraction with Firebase/Fake provider support
- Assembly Definitions

## Main Requirements

- C#
- Unity 2022.3.9f
- 2D gameplay
- Custom physics, no Unity Physics for gameplay movement/collisions
- Zenject
- SignalBus
- MVVM
- UniTask instead of coroutines
- Object Pool
- Factory-based object creation
- Configurable gameplay settings
- Firebase Analytics
- Ads adapter
- Assembly Definitions

## Controls

Keyboard:
- Turn left/right
- Thrust forward
- Fire bullet
- Fire laser

Mobile:
- TurnLeftButton
- TurnRightButton
- MoveButton
- FireButton
- LazerButton

Gameplay code depends on the common input abstraction and does not depend on a concrete input device.

## Patterns

### Factory Method / Abstract Factory

The project uses factory-based creation for runtime gameplay objects. Gameplay services do not manually construct complex objects directly. Instead, creation is delegated to dedicated factories.

Examples:
- `AsteroidInstanceFactory`
- `BulletInstanceFactory`
- `UfoInstanceFactory`
- `AsteroidInstanceZenjectFactory`
- `BulletInstanceZenjectFactory`
- `UfoInstanceZenjectFactory`

Why this pattern is used:
- Object creation requires several dependencies: model, body, collision body, view, settings.
- Factories keep creation logic in one place.
- Gameplay services do not need to know how an enemy or bullet is assembled.
- It works well with Zenject and object pools.

Object Pool is used separately and is not counted as one of the two GoF patterns.

### Strategy

The project uses Strategy for player input. Gameplay reads input through common abstractions and does not depend on keyboard, mouse, or mobile UI directly.

Examples:
- `IPlayerInput`
- `IPlayerInputSource`
- `KeyboardPlayerInput`
- `MobilePlayerInput`
- `PlayerInputRouter`
- `PlayerInputSourceResolver`

Why this pattern is used:
- Different platforms require different input sources.
- Keyboard and mobile controls can be changed independently.
- Gameplay code receives the same `PlayerInputState` regardless of device.
- New input sources, such as gamepad input, can be added without rewriting movement or shooting logic.

### Additional Architectural Pattern: Observer

Zenject `SignalBus` is used as an Observer-style event system for gameplay and infrastructure events.

Examples:
- `PlayerDamagedSignal`
- `PlayerDefeatedSignal`
- `EnemyDestroyedSignal`
- `PlayerLaserFiredSignal`
- `ScoreChangedSignal`

Why it is used:
- UI, analytics, ads, and gameplay systems can react to events without direct dependencies.
- Gameplay services do not call Firebase, ads, or UI directly.
- Event subscriptions are centralized through Zenject lifecycle interfaces.

This pattern is mentioned as an additional architectural solution, but the two required GoF patterns are Factory and Strategy.