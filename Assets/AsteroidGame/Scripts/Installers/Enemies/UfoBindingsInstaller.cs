using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Domain.Ufo.Models;
using AsteroidGame.Scripts.Domain.Ufo.Settings;
using AsteroidGame.Scripts.Gameplay.Ufo.Calculations;
using AsteroidGame.Scripts.Gameplay.Ufo.Contracts;
using AsteroidGame.Scripts.Gameplay.Ufo.Factories;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Pooling;
using AsteroidGame.Scripts.Gameplay.Ufo.Services;
using AsteroidGame.Scripts.Gameplay.Ufo.States;
using AsteroidGame.Scripts.Installers.SceneReferences;
using AsteroidGame.Scripts.Presentation.Ufo;

namespace AsteroidGame.Scripts.Installers.Enemies
{
    public sealed class UfoBindingsInstaller : Installer<IGameplaySceneReferences, UfoBindingsInstaller>
    {
        private readonly IGameplaySceneReferences _sceneReferences;

        public UfoBindingsInstaller(IGameplaySceneReferences sceneReferences) => _sceneReferences = sceneReferences;

        public override void InstallBindings()
        {
            if (_sceneReferences.UfoPrefab == null)
                throw new InvalidOperationException("UFO prefab is not assigned in GameInstaller.");

            if (_sceneReferences.GameplayRoot == null)
                throw new InvalidOperationException("Gameplay root is not assigned in GameInstaller.");

            Container.Bind<UfoSettings>().AsSingle();
            Container.Bind<UfoInstanceFactory>().AsSingle();
            Container.Bind<IUfoViewFactory>()
                .To<UfoViewFactoryAdapter>()
                .AsSingle()
                .WithArguments(_sceneReferences.UfoPrefab, _sceneReferences.GameplayRoot);
            Container.Bind<UfoTiltCalculator>().AsSingle();
            Container.Bind<UfoKnockbackMovement>().AsSingle();
            Container.Bind<UfoKnockbackService>().AsSingle();

            Container.BindInterfacesAndSelfTo<UfoPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<UfoSpawnService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UfoSimulationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UfoDestructionService>().AsSingle();

            Container.BindFactory<EnemyModel, UfoModel, UfoModelFactory>();
            Container.BindFactory<
                UfoModel,
                CollisionBody,
                IUfoView,
                UfoKnockbackState,
                UfoInstance,
                UfoInstanceZenjectFactory>();
            Container.BindFactory<UfoKnockbackState, UfoKnockbackStateFactory>();
        }
    }
}