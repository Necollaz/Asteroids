using System;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Asteroids.Models;
using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using UnityEngine;
using Zenject;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;
using AsteroidGame.Scripts.Domain.Bullets.Models;
using AsteroidGame.Scripts.Domain.Bullets.Settings;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Mapping;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Domain.Enemies.Rewards;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Calculations;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Physics.Services;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Domain.Player.Factories;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Domain.Score;
using AsteroidGame.Scripts.Domain.World;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Domain.World.Settings;
using AsteroidGame.Scripts.Gameplay.Asteroids.Calculations;
using AsteroidGame.Scripts.Gameplay.Asteroids.Contracts;
using AsteroidGame.Scripts.Gameplay.Asteroids.Factories;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Pooling;
using AsteroidGame.Scripts.Gameplay.Asteroids.Services;
using AsteroidGame.Scripts.Gameplay.Asteroids.Spawning;
using AsteroidGame.Scripts.Gameplay.Bullets.Contracts;
using AsteroidGame.Scripts.Gameplay.Bullets.Factories;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Pooling;
using AsteroidGame.Scripts.Gameplay.Bullets.Services;
using AsteroidGame.Scripts.Gameplay.Bullets.Timers;
using AsteroidGame.Scripts.Gameplay.Collision;
using AsteroidGame.Scripts.Gameplay.Enemies.Factories;
using AsteroidGame.Scripts.Gameplay.Factories;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Laser.Contracts;
using AsteroidGame.Scripts.Gameplay.Laser.Services;
using AsteroidGame.Scripts.Gameplay.Player.Calculations;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Gameplay.Player.Services;
using AsteroidGame.Scripts.Gameplay.Random;
using AsteroidGame.Scripts.Gameplay.Score;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Infrastructure.Configs;
using AsteroidGame.Scripts.Infrastructure.Random;
using AsteroidGame.Scripts.Infrastructure.Scenes;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Presentation.Asteroids;
using AsteroidGame.Scripts.Presentation.Asteroids.Effects;
using AsteroidGame.Scripts.Presentation.Bullets;
using AsteroidGame.Scripts.Presentation.Camera;
using AsteroidGame.Scripts.Presentation.Laser;
using AsteroidGame.Scripts.Presentation.Player.Presenters;
using AsteroidGame.Scripts.Presentation.Player.Views;
using AsteroidGame.Scripts.Signals.Bullets;
using AsteroidGame.Scripts.Signals.Enemies;
using AsteroidGame.Scripts.Signals.Game;
using AsteroidGame.Scripts.Signals.Player;
using AsteroidGame.Scripts.Signals.Score;
using AsteroidGame.Scripts.UI.Common;
using AsteroidGame.Scripts.UI.Game;
using AsteroidGame.Scripts.UI.Player;

namespace AsteroidGame.Scripts.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameplaySettingsConfig _gameplaySettingsConfig;
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private Transform _container;
        
        [Header("Asteroids")]
        [SerializeField] private AsteroidView _largeAsteroidPrefab;
        [SerializeField] private AsteroidView _mediumAsteroidPrefab;
        [SerializeField] private AsteroidView _smallAsteroidPrefab;
        [SerializeField] private AsteroidExplosionView _asteroidExplosionPrefab;
        
        public override void InstallBindings()
        {
            InstallSignalBus();
            InstallSettings();
            InstallPhysics();
            InstallWorld();
            InstallGameState();
            InstallCollision();
            InstallPlayer();
            InstallBullets();
            InstallAsteroids();
            InstallAsteroidEffects();
            InstallLaser();
            InstallScore();
            InstallPresentation();
            InstallUi();
            InstallInfrastructure();
            InstallInput();
        }

        private void InstallSignalBus()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PlayerDamagedSignal>();
            Container.DeclareSignal<PlayerDefeatedSignal>();
            Container.DeclareSignal<PlayerInvulnerabilityStartedSignal>();
            Container.DeclareSignal<PlayerInvulnerabilityEndedSignal>();
            Container.DeclareSignal<GameDefeatStartedSignal>();
            Container.DeclareSignal<GameRestartRequestedSignal>();
            Container.DeclareSignal<PlayerBulletFiredSignal>().OptionalSubscriber();
            Container.DeclareSignal<EnemyHitByBulletSignal>().OptionalSubscriber();
            Container.DeclareSignal<PlayerLaserFiredSignal>();
            Container.DeclareSignal<PlayerLaserChargesChangedSignal>();
            Container.DeclareSignal<EnemyHitByLaserSignal>().OptionalSubscriber();
            Container.DeclareSignal<EnemyDestroyedSignal>().OptionalSubscriber();
            Container.DeclareSignal<ScoreChangedSignal>().OptionalSubscriber();

            Container.Bind<PlayerBulletFiredSignal>().AsSingle();
        }

        private void InstallSettings()
        {
            if (_gameplaySettingsConfig == null)
                throw new InvalidOperationException("GameplaySettingsConfig is not assigned in GameInstaller.");

            Container.Bind(
                    typeof(GameplaySettingsConfig),
                    typeof(IPlayerMovementSettingsData),
                    typeof(IPlayerCollisionSettingsData),
                    typeof(IPlayerLaserSettingsData),
                    typeof(IKeyboardInputSettingsData),
                    typeof(IBulletSettingsData), 
                    typeof(IAsteroidSettingsData), 
                    typeof(IEnemyRewardSettingsData))
                .FromInstance(_gameplaySettingsConfig)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<GameplaySettingsConfigValidator>().AsSingle();
        }

        private void InstallPhysics()
        {
            Container.Bind<PhysicsValueFactory>().AsSingle();
            Container.Bind<Direction2DCalculator>().AsSingle();
            Container.Bind<CustomPhysicsWorld>().AsSingle();

            Container.BindFactory<Vector2D, Velocity, float, Body2D, Body2DFactory>();
        }

        private void InstallWorld()
        {
            Container.Bind<CameraBoundsView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IWorldSettingsData>().To<CameraBoundsView>().FromResolve();
            Container.Bind<WorldSettings>().AsSingle();
            Container.Bind<WorldBounds>().AsSingle();
        }

        private void InstallGameState() => Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();

        private void InstallCollision()
        {
            Container.Bind<CircleCollisionDetector>().AsSingle();
            Container.Bind<CollisionBodyRegistry>().AsSingle();
            Container.Bind<CollisionCategoryPolicy>().AsSingle();
            Container.Bind<PlayerEnemyCollisionContactResolver>().AsSingle();
            Container.Bind<PlayerEnemyCollisionHandler>().AsSingle();
            Container.Bind<CollisionContactRouter>().AsSingle();
            Container.Bind<BulletEnemyCollisionContactResolver>().AsSingle();
            Container.Bind<BulletEnemyCollisionHandler>().AsSingle();
            Container.Bind<LineCircleIntersectionDetector>().AsSingle();

            Container.BindInterfacesAndSelfTo<CollisionSimulationService>().AsSingle();

            Container.BindFactory<CollisionCategory, Body2D, float, CollisionBody, CollisionBodyFactory>();
        }

        private void InstallPlayer()
        {
            Container.Bind<PlayerMovementSettings>().AsSingle();
            Container.Bind<PlayerCollisionSettings>().AsSingle();
            Container.Bind<PlayerLaserSettings>().AsSingle();

            Container.Bind<PlayerHealthState>().AsSingle();
            Container.Bind<PlayerInvulnerabilityState>().AsSingle();
            Container.Bind<PlayerLaserMagazine>().AsSingle();
            Container.Bind<PlayerLaserRechargeState>().AsSingle();
            Container.Bind<PlayerSnapshotFactory>().AsSingle();

            Container.Bind<ITimeProvider>().To<UnityTimeProvider>().AsSingle();
            Container.Bind<PlayerAccelerationCalculator>().AsSingle();
            Container.Bind<IPlayerControlState>().To<PlayerControlStateProvider>().AsSingle();

            Container.BindFactory<Body2D, PlayerModel, PlayerModelFactory>();

            Container.Bind<PlayerModel>().FromMethod(CreatePlayerModel).AsSingle();
            Container.Bind<PlayerDamageService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerMovementService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerCollisionBodyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInvulnerabilityTimerService>().AsSingle();
        }

        private void InstallBullets()
        {
            if (_bulletPrefab == null)
                throw new InvalidOperationException("Bullet prefab is not assigned in GameInstaller.");

            if (_container == null)
                throw new InvalidOperationException("Container is not assigned in GameInstaller.");

            Container.Bind<BulletSettings>().AsSingle();
            Container.Bind<BulletFireCooldown>().AsSingle();
            Container.Bind<BulletInstanceFactory>().AsSingle();
            Container.Bind<IBulletViewFactory>().To<BulletViewFactoryAdapter>().AsSingle();

            Container.BindInterfacesAndSelfTo<BulletPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerBulletShootingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletSimulationService>().AsSingle();

            Container.BindFactory<Body2D, BulletModel, BulletModelFactory>();
            Container.BindFactory<BulletView, BulletViewPrefabFactory>()
                .FromComponentInNewPrefab(_bulletPrefab)
                .UnderTransform(_container);
            Container.BindFactory<
                BulletModel,
                CollisionBody,
                IBulletView,
                BulletInstance,
                BulletInstanceZenjectFactory>();
        }
        
        private void InstallAsteroids()
        {
            if (_largeAsteroidPrefab == null)
                throw new InvalidOperationException("Large asteroid prefab is not assigned in GameInstaller.");

            if (_mediumAsteroidPrefab == null)
                throw new InvalidOperationException("Medium asteroid prefab is not assigned in GameInstaller.");

            if (_smallAsteroidPrefab == null)
                throw new InvalidOperationException("Small asteroid prefab is not assigned in GameInstaller.");

            if (_container == null)
                throw new InvalidOperationException("Container is not assigned in GameInstaller.");

            Container.Bind<AsteroidSettings>().AsSingle();
            Container.Bind<EnemyCollisionCategoryMapper>().AsSingle();
            Container.Bind<AsteroidSpawnPointSelector>().AsSingle();
            Container.Bind<AsteroidVelocityStabilizer>().AsSingle();
            Container.Bind<AsteroidInstanceFactory>().AsSingle();
            Container.Bind<IRandomValueProvider>().To<UnityRandomValueProvider>().AsSingle();
            Container.Bind<IAsteroidViewFactory>()
                .To<AsteroidViewFactoryAdapter>()
                .AsSingle()
                .WithArguments(
                    _largeAsteroidPrefab,
                    _mediumAsteroidPrefab,
                    _smallAsteroidPrefab,
                    _container);

            Container.BindInterfacesAndSelfTo<AsteroidPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidSpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidSimulationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidDestructionService>().AsSingle();
            
            Container.BindFactory<EnemyType, Body2D, EnemyModel, EnemyModelFactory>();
            Container.BindFactory<EnemyModel, AsteroidModel, AsteroidModelFactory>();
            Container.BindFactory<
                AsteroidModel,
                CollisionBody,
                IAsteroidView,
                AsteroidInstance,
                AsteroidInstanceZenjectFactory>();
        }
        
        private void InstallAsteroidEffects()
        {
            if (_asteroidExplosionPrefab == null)
                throw new InvalidOperationException("Asteroid explosion prefab is not assigned in GameInstaller.");

            if (_container == null)
                throw new InvalidOperationException("Effects root is not assigned in GameInstaller.");

            Container.BindFactory<AsteroidExplosionView, AsteroidExplosionViewPrefabFactory>()
                .FromComponentInNewPrefab(_asteroidExplosionPrefab)
                .UnderTransform(_container);
            Container.BindFactory<AsteroidExplosionView, AsteroidExplosionInstance, AsteroidExplosionInstanceFactory>();
            
            Container.BindInterfacesAndSelfTo<AsteroidExplosionPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidExplosionPresenter>().AsSingle();
        }
        
        private void InstallLaser()
        {
            Container
                .Bind<ILaserSpawnPointProvider>()
                .To<PlayerLaserSpawnPointView>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserShootingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserRechargeService>().AsSingle();
        }
        
        private void InstallScore()
        {
            Container.Bind<ScoreState>().AsSingle();
            Container.Bind<EnemyRewardTable>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyRewardService>().AsSingle();
        }

        private void InstallPresentation()
        {
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerViewPresenter>().AsSingle();
            
            Container.Bind<PlayerInvulnerabilityEffectView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInvulnerabilityPresenter>().AsSingle();
            
            Container.Bind<PlayerLaserView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserVisualPresenter>().AsSingle();
        }

        private void InstallUi()
        {
            Container.Bind<UiEventSystemView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UiEventSystemPresenter>().AsSingle();

            Container.Bind<PlayerHealthView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHealthPresenter>().AsSingle();
            
            Container.Bind<DefeatGameView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<DefeatGamePresenter>().AsSingle();
            
            Container.Bind<PlayerLaserHudView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserHudPresenter>().AsSingle();
        }

        private void InstallInfrastructure() => Container.BindInterfacesAndSelfTo<SceneRestartService>().AsSingle();

        private void InstallInput() => Container.Bind<IPlayerInput>().FromComponentInHierarchy().AsSingle();

        private PlayerModel CreatePlayerModel(InjectContext context)
        {
            DiContainer container = context.Container;
            PlayerMovementSettings movementSettings = container.Resolve<PlayerMovementSettings>();
            Body2DFactory bodyFactory = container.Resolve<Body2DFactory>();
            PlayerModelFactory playerModelFactory = container.Resolve<PlayerModelFactory>();

            Body2D body = bodyFactory.Create(
                movementSettings.SpawnPosition,
                movementSettings.InitialVelocity,
                movementSettings.SpawnRotationDegrees);

            return playerModelFactory.Create(body);
        }
    }
}