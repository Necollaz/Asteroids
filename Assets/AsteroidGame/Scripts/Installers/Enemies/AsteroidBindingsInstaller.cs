using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Asteroids.Models;
using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Calculations;
using AsteroidGame.Scripts.Gameplay.Asteroids.Contracts;
using AsteroidGame.Scripts.Gameplay.Asteroids.Factories;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Pooling;
using AsteroidGame.Scripts.Gameplay.Asteroids.Services;
using AsteroidGame.Scripts.Gameplay.Enemies.Spawning;
using AsteroidGame.Scripts.Installers.SceneReferences;
using AsteroidGame.Scripts.Presentation.Asteroids;

namespace AsteroidGame.Scripts.Installers.Enemies
{
    public sealed class AsteroidBindingsInstaller : Installer<IGameplaySceneReferences, AsteroidBindingsInstaller>
    {
        private readonly IGameplaySceneReferences _sceneReferences;

        public AsteroidBindingsInstaller(IGameplaySceneReferences sceneReferences) => 
            _sceneReferences = sceneReferences;

        public override void InstallBindings()
        {
            if (_sceneReferences.LargeAsteroidPrefab == null)
                throw new InvalidOperationException("Large asteroid prefab is not assigned in GameInstaller.");

            if (_sceneReferences.MediumAsteroidPrefab == null)
                throw new InvalidOperationException("Medium asteroid prefab is not assigned in GameInstaller.");

            if (_sceneReferences.SmallAsteroidPrefab == null)
                throw new InvalidOperationException("Small asteroid prefab is not assigned in GameInstaller.");

            if (_sceneReferences.GameplayRoot == null)
                throw new InvalidOperationException("Gameplay root is not assigned in GameInstaller.");

            Container.Bind<AsteroidSettings>().AsSingle();
            Container.Bind<AsteroidSpawnPointSelector>().AsSingle();
            Container.Bind<AsteroidVelocityStabilizer>().AsSingle();
            Container.Bind<AsteroidInstanceFactory>().AsSingle();
            Container.Bind<IAsteroidViewFactory>()
                .To<AsteroidViewFactoryAdapter>()
                .AsSingle()
                .WithArguments(
                    _sceneReferences.LargeAsteroidPrefab,
                    _sceneReferences.MediumAsteroidPrefab,
                    _sceneReferences.SmallAsteroidPrefab,
                    _sceneReferences.GameplayRoot);

            Container.BindInterfacesAndSelfTo<AsteroidPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidSpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidSimulationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidDestructionService>().AsSingle();

            Container.BindFactory<EnemyModel, AsteroidModel, AsteroidModelFactory>();
            Container.BindFactory<
                AsteroidModel,
                CollisionBody,
                IAsteroidView,
                AsteroidInstance,
                AsteroidInstanceZenjectFactory>();
        }
    }
}