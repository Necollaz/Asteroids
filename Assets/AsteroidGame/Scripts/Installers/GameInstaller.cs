using System;
using UnityEngine;
using Zenject;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;
using AsteroidGame.Scripts.Domain.Bullets.Models;
using AsteroidGame.Scripts.Domain.Bullets.Settings;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Physics.Calculations;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Physics.Services;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Domain.Player.Factories;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Domain.World;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Domain.World.Settings;
using AsteroidGame.Scripts.Gameplay.Bullets.Contracts;
using AsteroidGame.Scripts.Gameplay.Bullets.Factories;
using AsteroidGame.Scripts.Gameplay.Bullets.Pooling;
using AsteroidGame.Scripts.Gameplay.Bullets.Services;
using AsteroidGame.Scripts.Gameplay.Bullets.Timers;
using AsteroidGame.Scripts.Gameplay.Collision;
using AsteroidGame.Scripts.Gameplay.Factories;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Player.Calculations;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Gameplay.Player.Services;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Infrastructure.Configs;
using AsteroidGame.Scripts.Infrastructure.Scenes;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Presentation.Bullets;
using AsteroidGame.Scripts.Presentation.Camera;
using AsteroidGame.Scripts.Presentation.Player.Presenters;
using AsteroidGame.Scripts.Presentation.Player.Views;
using AsteroidGame.Scripts.Signals.Bullets;
using AsteroidGame.Scripts.Signals.Enemies;
using AsteroidGame.Scripts.Signals.Game;
using AsteroidGame.Scripts.Signals.Player;
using AsteroidGame.Scripts.UI.Common;
using AsteroidGame.Scripts.UI.Game;
using AsteroidGame.Scripts.UI.Player;

namespace AsteroidGame.Scripts.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameplaySettingsConfig _gameplaySettingsConfig;
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private Transform _bulletRoot;
        
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
                    typeof(IBulletSettingsData))
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

            if (_bulletRoot == null)
                throw new InvalidOperationException("Bullet root is not assigned in GameInstaller.");

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
                .UnderTransform(_bulletRoot);
        }

        private void InstallPresentation()
        {
            Container.BindInterfacesAndSelfTo<PlayerViewPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInvulnerabilityPresenter>().AsSingle();

            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerInvulnerabilityEffectView>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallUi()
        {
            Container.Bind<UiEventSystemView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UiEventSystemPresenter>().AsSingle();

            Container.Bind<PlayerHealthView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHealthPresenter>().AsSingle();
            
            Container.Bind<DefeatGameView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<DefeatGamePresenter>().AsSingle();
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