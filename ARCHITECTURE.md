# Architecture

This project is a 2D Asteroids clone built for Unity 2022.3.9f.

The main architectural goal is to separate gameplay logic from Unity presentation code. Most gameplay decisions are implemented in plain C# classes, while `MonoBehaviour` classes are used as scene/prefab bridges.

## Layers

### Domain

Path:

`Assets/AsteroidGame/Scripts/Domain`

The Domain layer contains pure game data, models, value objects, settings, and calculations that do not depend on scene objects or UI.

Examples:
- `PlayerModel`
- `EnemyModel`
- `AsteroidModel`
- `UfoModel`
- `Body2D`
- `Vector2D`
- `Velocity`
- `Acceleration`
- `CollisionBody`
- `CircleCollisionDetector`
- `LineCircleIntersectionDetector`
- `EnemyRewardTable`

Responsibilities:
- Store core gameplay state.
- Provide small calculation classes.
- Describe collision bodies and custom physics data.
- Keep enemy types and reward data.
- Avoid Unity scene dependencies.

### Gameplay

Path:

`Assets/AsteroidGame/Scripts/Gameplay`

The Gameplay layer contains runtime game services and gameplay flows. These classes coordinate domain models, pools, factories, collisions, shooting, spawning, and player/enemy behavior.

Examples:
- `PlayerMovementService`
- `PlayerDamageService`
- `PlayerInvulnerabilityTimerService`
- `BulletSimulationService`
- `PlayerBulletShootingService`
- `PlayerLaserShootingService`
- `PlayerLaserDamageService`
- `AsteroidSpawnService`
- `AsteroidDestructionService`
- `UfoSpawnService`
- `UfoSimulationService`
- `CollisionSimulationService`
- `EnemyFacade`

Responsibilities:
- Run gameplay through Zenject `ITickable`, `IFixedTickable`, `IInitializable`, and `IDisposable`.
- Move objects using custom physics.
- Detect and resolve gameplay collisions without Unity Physics callbacks.
- Spawn and despawn enemies through factories and pools.
- Fire gameplay signals through Zenject `SignalBus`.
- Keep gameplay independent from concrete UI and SDK implementations.

### Presentation

Path:

`Assets/AsteroidGame/Scripts/Presentation`

The Presentation layer contains Unity-facing visual classes. These classes are allowed to inherit from `MonoBehaviour`, but they should only display state or provide scene/prefab references.

Examples:
- `PlayerView`
- `PlayerViewPresenter`
- `PlayerInvulnerabilityEffectView`
- `AsteroidView`
- `UfoView`
- `BulletView`
- `PlayerLaserView`
- `PlayerLaserSpawnPointView`
- `CameraBoundsView`

Responsibilities:
- Render positions, rotations, effects, and prefab visuals.
- Provide scene references such as camera bounds or laser spawn point.
- Keep visual logic separate from gameplay rules.
- Avoid direct gameplay decisions inside View classes.

### UI

Path:

`Assets/AsteroidGame/Scripts/UI`

The UI layer follows MVVM.

Examples:
- `PlayerStatsHudView`
- `PlayerStatsHudPresenter`
- `PlayerStatsHudViewModel`
- `PlayerHealthView`
- `PlayerHealthPresenter`
- `PlayerHealthViewModel`
- `DefeatGameView`
- `DefeatGamePresenter`
- `DefeatGameViewModel`

Responsibilities:
- Views display already prepared data.
- ViewModels store display-ready data.
- Presenters listen to signals or read models and update views.
- UI buttons send signals instead of changing gameplay directly.
- UI does not restart scenes, damage the player, spawn enemies, or change gameplay rules directly.

### Input

Path:

`Assets/AsteroidGame/Scripts/Input`

The Input layer hides concrete input devices behind common interfaces.

Examples:
- `IPlayerInput`
- `IPlayerInputSource`
- `KeyboardPlayerInput`
- `MobilePlayerInput`
- `PlayerInputRouter`
- `PlayerInputSourceResolver`
- `MobilePlayerInputView`

Responsibilities:
- Read keyboard and mobile controls.
- Convert device-specific input into `PlayerInputState`.
- Let gameplay depend on `IPlayerInput` instead of a concrete platform.
- Support switching input source through config.

### Infrastructure

Path:

`Assets/AsteroidGame/Scripts/Infrastructure`

The Infrastructure layer contains integrations with external systems and platform-specific services.

Examples:
- `SceneRestartService`
- `UnityRandomValueProvider`
- `AdsInitializationService`
- `FakeAdsService`
- `AdMobAdsService`
- `AnalyticsInitializationService`
- `FakeAnalyticsService`
- `FirebaseAnalyticsService`
- `GameplayJsonSettingsLoader`

Responsibilities:
- Initialize ads and analytics.
- Load and validate gameplay settings.
- Restart scene through a signal-driven service.
- Provide Unity-specific implementations of infrastructure contracts.
- Keep SDK calls outside gameplay classes.

### Signals

Path:

`Assets/AsteroidGame/Scripts/Signals`

Signals are small data classes used with Zenject `SignalBus`.

Examples:
- `PlayerDamagedSignal`
- `PlayerDefeatedSignal`
- `PlayerInvulnerabilityStartedSignal`
- `PlayerInvulnerabilityEndedSignal`
- `EnemyHitByBulletSignal`
- `EnemyHitByLaserSignal`
- `EnemyDestroyedSignal`
- `ScoreChangedSignal`
- `GameRestartRequestedSignal`

Responsibilities:
- Decouple gameplay systems.
- Let UI, analytics, ads, and presentation react to gameplay events.
- Avoid direct calls from gameplay to UI or infrastructure.

### Installers

Path:

`Assets/AsteroidGame/Scripts/Installers`

Installers configure Zenject bindings and scene references.

Examples:
- `GameInstaller`
- `GameSignalsInstaller`
- `GameSettingsInstaller`
- `PhysicsBindingsInstaller`
- `PlayerBindingsInstaller`
- `BulletBindingsInstaller`
- `LaserBindingsInstaller`
- `AsteroidBindingsInstaller`
- `UfoBindingsInstaller`
- `UiBindingsInstaller`
- `InfrastructureBindingsInstaller`

Responsibilities:
- Bind services, models, factories, pools, presenters, and settings.
- Register SignalBus signals.
- Keep scene references in one explicit place.
- Prevent gameplay code from creating dependencies manually.

## Dependency Direction

The project uses Assembly Definitions to keep dependencies controlled.

Expected dependency direction:

```text
Domain
  ↑
Gameplay
  ↑
Presentation / UI / Infrastructure
  ↑
Installers