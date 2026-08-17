using System;
using Zenject;
using AsteroidGame.Scripts.Installers.SceneReferences;
using AsteroidGame.Scripts.Presentation.Asteroids.Effects;
using AsteroidGame.Scripts.Presentation.Asteroids.Effects.Factories;
using AsteroidGame.Scripts.Presentation.Ufo.Effects;
using AsteroidGame.Scripts.Presentation.Ufo.Effects.Factories;

namespace AsteroidGame.Scripts.Installers.Presentation
{
    public sealed class EffectsBindingsInstaller : Installer<IGameplaySceneReferences, EffectsBindingsInstaller>
    {
        private readonly IGameplaySceneReferences _sceneReferences;

        public EffectsBindingsInstaller(IGameplaySceneReferences sceneReferences) => _sceneReferences = sceneReferences;

        public override void InstallBindings()
        {
            InstallAsteroidEffects();
            InstallUfoEffects();
        }

        private void InstallAsteroidEffects()
        {
            if (_sceneReferences.AsteroidExplosionPrefab == null)
                throw new InvalidOperationException("Asteroid explosion prefab is not assigned in GameInstaller.");

            if (_sceneReferences.GameplayRoot == null)
                throw new InvalidOperationException("Gameplay root is not assigned in GameInstaller.");

            Container.BindFactory<AsteroidExplosionView, AsteroidExplosionViewPrefabFactory>()
                .FromComponentInNewPrefab(_sceneReferences.AsteroidExplosionPrefab)
                .UnderTransform(_sceneReferences.GameplayRoot);
            Container.BindFactory<AsteroidExplosionView, AsteroidExplosionInstance, AsteroidExplosionInstanceFactory>();

            Container.BindInterfacesAndSelfTo<AsteroidExplosionPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<AsteroidExplosionPresenter>().AsSingle();
        }

        private void InstallUfoEffects()
        {
            if (_sceneReferences.UfoExplosionPrefab == null)
                throw new InvalidOperationException("UFO explosion prefab is not assigned in GameInstaller.");

            if (_sceneReferences.GameplayRoot == null)
                throw new InvalidOperationException("Gameplay root is not assigned in GameInstaller.");

            Container.BindFactory<UfoExplosionView, UfoExplosionViewPrefabFactory>()
                .FromComponentInNewPrefab(_sceneReferences.UfoExplosionPrefab)
                .UnderTransform(_sceneReferences.GameplayRoot);
            Container.BindFactory<UfoExplosionView, UfoExplosionInstance, UfoExplosionInstanceFactory>();

            Container.BindInterfacesAndSelfTo<UfoExplosionPool>().AsSingle();
            Container.BindInterfacesAndSelfTo<UfoExplosionPresenter>().AsSingle();
        }
    }
}