using System;
using UnityEngine;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision;
using AsteroidGame.Scripts.Domain.Physics;
using AsteroidGame.Scripts.Domain.Player;
using AsteroidGame.Scripts.Domain.World;
using AsteroidGame.Scripts.Gameplay.Collision;
using AsteroidGame.Scripts.Gameplay.Factories;
using AsteroidGame.Scripts.Gameplay.Player;
using AsteroidGame.Scripts.Gameplay.Player.Services;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Infrastructure.Configs;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Presentation.Camera;
using AsteroidGame.Scripts.Presentation.Player;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Installers
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameplaySettingsConfig _gameplaySettingsConfig;
        
        public override void InstallBindings()
        {
            InstallSignalBus();
            InstallSettings();
            InstallPhysics();
            InstallWorld();
            InstallCollision();
            InstallPlayer();
            InstallPresentation();
            InstallInput();
        }

        private void InstallSignalBus()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PlayerDamagedSignal>();
            Container.DeclareSignal<PlayerInvulnerabilityStartedSignal>();
            Container.DeclareSignal<PlayerInvulnerabilityEndedSignal>();
        }

        private void InstallSettings()
        {
            if (_gameplaySettingsConfig == null)
                throw new InvalidOperationException("GameplaySettingsConfig is not assigned in GameInstaller.");

            Container.Bind(
                    typeof(GameplaySettingsConfig),
                    typeof(IPlayerMovementSettingsData),
                    typeof(IPlayerCollisionSettingsData),
                    typeof(IKeyboardInputSettingsData))
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
        
        private void InstallCollision()
        {
            Container.Bind<CircleCollisionDetector>().AsSingle();
            Container.Bind<CollisionBodyRegistry>().AsSingle();
            Container.Bind<CollisionCategoryPolicy>().AsSingle();
            Container.Bind<PlayerEnemyCollisionHandler>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CollisionSimulationService>().AsSingle();

            Container.BindFactory<CollisionCategory, Body2D, float, CollisionBody, CollisionBodyFactory>();
        }

        private void InstallPlayer()
        {
            Container.Bind<PlayerMovementSettings>().AsSingle();
            Container.Bind<PlayerCollisionSettings>().AsSingle();
            Container.Bind<ITimeProvider>().To<UnityTimeProvider>().AsSingle();
            Container.Bind<PlayerAccelerationCalculator>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInvulnerabilityService>().AsSingle();
            
            Container.Bind<PlayerHealthService>().AsSingle();
            Container.Bind<PlayerDamageService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PlayerMovementService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerCollisionBodyService>().AsSingle();
        }

        private void InstallPresentation()
        {
            Container.BindInterfacesTo<PlayerViewPresenter>().AsSingle();
            Container.BindInterfacesTo<PlayerInvulnerabilityPresenter>().AsSingle();

            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerInvulnerabilityEffectView>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallInput() => Container.Bind<IPlayerInput>().FromComponentInHierarchy().AsSingle();
    }
}