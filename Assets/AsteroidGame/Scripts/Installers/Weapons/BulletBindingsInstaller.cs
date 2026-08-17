using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Bullets.Models;
using AsteroidGame.Scripts.Domain.Bullets.Settings;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Contracts;
using AsteroidGame.Scripts.Gameplay.Bullets.Factories;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Pooling;
using AsteroidGame.Scripts.Gameplay.Bullets.Services;
using AsteroidGame.Scripts.Gameplay.Bullets.Timers;
using AsteroidGame.Scripts.Installers.SceneReferences;
using AsteroidGame.Scripts.Presentation.Bullets;

namespace AsteroidGame.Scripts.Installers.Weapons
{
    public sealed class BulletBindingsInstaller : Installer<IGameplaySceneReferences, BulletBindingsInstaller>
    {
        private readonly IGameplaySceneReferences _sceneReferences;

        public BulletBindingsInstaller(IGameplaySceneReferences sceneReferences) => _sceneReferences = sceneReferences;

        public override void InstallBindings()
        {
            if (_sceneReferences.BulletPrefab == null)
                throw new InvalidOperationException("Bullet prefab is not assigned in GameInstaller.");

            if (_sceneReferences.GameplayRoot == null)
                throw new InvalidOperationException("Gameplay root is not assigned in GameInstaller.");

            Container.Bind<BulletSettings>().AsSingle();
            Container.Bind<BulletFireCooldown>().AsSingle();
            Container.Bind<BulletInstanceFactory>().AsSingle();
            Container.Bind<IBulletViewFactory>().To<BulletViewFactoryAdapter>().AsSingle();

            Container.BindInterfacesAndSelfTo<BulletPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerBulletShootingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<BulletSimulationService>().AsSingle();

            Container.BindFactory<Body2D, BulletModel, BulletModelFactory>();
            Container.BindFactory<BulletView, BulletViewPrefabFactory>()
                .FromComponentInNewPrefab(_sceneReferences.BulletPrefab)
                .UnderTransform(_sceneReferences.GameplayRoot);
            Container.BindFactory<
                BulletModel,
                CollisionBody,
                IBulletView,
                BulletInstance,
                BulletInstanceZenjectFactory>();
        }
    }
}