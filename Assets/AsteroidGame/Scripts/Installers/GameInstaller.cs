using System;
using Zenject;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Physics;
using AsteroidGame.Scripts.Domain.Player;
using AsteroidGame.Scripts.Domain.World;
using AsteroidGame.Scripts.Gameplay.Factories;
using AsteroidGame.Scripts.Gameplay.Player;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Infrastructure.Configs;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Presentation.Camera;
using AsteroidGame.Scripts.Presentation.Player;

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
            InstallPlayer();
            InstallPresentation();
            InstallInput();
        }

        private void InstallSignalBus() => SignalBusInstaller.Install(Container);

        private void InstallSettings()
        {
            if (_gameplaySettingsConfig == null)
                throw new InvalidOperationException("GameplaySettingsConfig is not assigned in GameInstaller.");

            Container.Bind(
                    typeof(GameplaySettingsConfig),
                    typeof(IPlayerMovementSettingsData),
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

        private void InstallPlayer()
        {
            Container.Bind<PlayerMovementSettings>().AsSingle();
            Container.Bind<ITimeProvider>().To<UnityTimeProvider>().AsSingle();
            Container.Bind<PlayerAccelerationCalculator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovementService>().AsSingle();
        }

        private void InstallPresentation()
        {
            Container.BindInterfacesTo<PlayerViewPresenter>().AsSingle();
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
        }

        private void InstallInput() => Container.Bind<IPlayerInput>().FromComponentInHierarchy().AsSingle();
    }
}