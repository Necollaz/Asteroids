using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Domain.Ufo.Contracts;
using AsteroidGame.Scripts.Domain.World;
using AsteroidGame.Scripts.Infrastructure.Configs;
using AsteroidGame.Scripts.Infrastructure.Configs.Json;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Loading;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Validation;
using AsteroidGame.Scripts.Input.Contracts;
using AsteroidGame.Scripts.Installers.SceneReferences;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class GameSettingsInstaller : Installer<IGameplaySceneReferences, GameSettingsInstaller>
    {
        private readonly IGameplaySceneReferences _sceneReferences;

        public GameSettingsInstaller(IGameplaySceneReferences sceneReferences) => _sceneReferences = sceneReferences;

        public override void InstallBindings()
        {
            GameplaySettingsConfig config = _sceneReferences.GameplaySettingsConfig;

            if (config == null)
                throw new InvalidOperationException("GameplaySettingsConfig is not assigned in GameInstaller.");

            Container.Bind<GameplaySettingsConfig>().FromInstance(config).AsSingle();

            if (config.SettingsSource == GameplaySettingsSource.Json)
            {
                BindJsonSettings(config);
                
                return;
            }

            BindScriptableObjectSettings();
        }

        private void BindJsonSettings(GameplaySettingsConfig config)
        {
            Container.Bind<KeyCodeParser>().AsSingle();
            Container.Bind<GameplayJsonSettingsValidator>().AsSingle();
            Container.Bind<GameplayJsonSettingsLoader>().AsSingle();
            Container.Bind<GameplayJsonSettings>()
                .FromMethod(context => context.Container.Resolve<GameplayJsonSettingsLoader>().Load(config))
                .AsSingle();

            BindSettingsInterfaces<GameplayJsonSettings>();
        }

        private void BindScriptableObjectSettings()
        {
            BindSettingsInterfaces<GameplaySettingsConfig>();
            Container.BindInterfacesAndSelfTo<GameplaySettingsConfigValidator>().AsSingle();
        }

        private void BindSettingsInterfaces<TSettings>()
            where TSettings :
            IPlayerMovementSettingsData,
            IPlayerCollisionSettingsData,
            IPlayerLaserSettingsData,
            IKeyboardInputSettingsData,
            IPlayerInputRouterSettingsData,
            IMobileInputSettingsData,
            IBulletSettingsData,
            IAsteroidSettingsData,
            IEnemyRewardSettingsData,
            IUfoSettingsData,
            IWorldSettingsData
        {
            Container.Bind<IPlayerMovementSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IPlayerCollisionSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IPlayerLaserSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IKeyboardInputSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IPlayerInputRouterSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IMobileInputSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IBulletSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IAsteroidSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IEnemyRewardSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IUfoSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IWorldSettingsData>().To<TSettings>().FromResolve();
        }
    }
}