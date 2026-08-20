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
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Factories;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Loading;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Validation;
using AsteroidGame.Scripts.Infrastructure.Configs.Sections;
using AsteroidGame.Scripts.Infrastructure.Configs.Validation;
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
            BindCommonValidators();

            if (config.SettingsSource == GameplaySettingsSource.Json)
            {
                BindJsonSettings(config);

                return;
            }

            BindScriptableObjectSettings(config);
        }

        private void BindCommonValidators()
        {
            Container.Bind<PlayerSettingsValidator>().AsSingle();
            Container.Bind<WeaponSettingsValidator>().AsSingle();
            Container.Bind<EnemySettingsValidator>().AsSingle();
            Container.Bind<SpawnSettingsValidator>().AsSingle();
            Container.Bind<WorldSettingsValidator>().AsSingle();
        }

        private void BindJsonSettings(GameplaySettingsConfig config)
        {
            Container.Bind<KeyCodeParser>().AsSingle();
            Container.Bind<GameplayJsonSettingsFactory>().AsSingle();
            Container.Bind<GameplayJsonSettingsValidator>().AsSingle();
            Container.Bind<GameplayJsonSettingsLoader>().AsSingle();
            Container.Bind<GameplayJsonSettings>()
                .FromMethod(context => context.Container.Resolve<GameplayJsonSettingsLoader>().Load(config))
                .AsSingle();
            Container.Bind<JsonPlayerSettingsSection>()
                .FromResolveGetter<GameplayJsonSettings>(settings => settings.Player)
                .AsSingle();
            Container.Bind<JsonWeaponSettingsSection>()
                .FromResolveGetter<GameplayJsonSettings>(settings => settings.Weapons)
                .AsSingle();
            Container.Bind<JsonEnemySettingsSection>()
                .FromResolveGetter<GameplayJsonSettings>(settings => settings.Enemies)
                .AsSingle();
            Container.Bind<JsonWorldSettingsSection>()
                .FromResolveGetter<GameplayJsonSettings>(settings => settings.World)
                .AsSingle();
            Container.Bind<JsonEnemySpawnSettingsSection>()
                .FromResolveGetter<GameplayJsonSettings>(settings => settings.Spawn)
                .AsSingle();

            BindPlayerSettings<JsonPlayerSettingsSection>();
            BindWeaponSettings<JsonWeaponSettingsSection>();
            BindEnemySettings<JsonEnemySettingsSection>();
            BindWorldSettings<JsonWorldSettingsSection>();
            BindSpawnSettings<JsonEnemySpawnSettingsSection>();
        }

        private void BindScriptableObjectSettings(GameplaySettingsConfig config)
        {
            Container.Bind<PlayerSettingsConfigSection>().FromInstance(config.Player).AsSingle();
            Container.Bind<WeaponSettingsConfigSection>().FromInstance(config.Weapons).AsSingle();
            Container.Bind<EnemySettingsConfigSection>().FromInstance(config.Enemies).AsSingle();
            Container.Bind<WorldSettingsConfigSection>().FromInstance(config.World).AsSingle();
            Container.Bind<SpawnSettingsConfigSection>().FromInstance(config.Spawning).AsSingle();

            BindPlayerSettings<PlayerSettingsConfigSection>();
            BindWeaponSettings<WeaponSettingsConfigSection>();
            BindEnemySettings<EnemySettingsConfigSection>();
            BindWorldSettings<WorldSettingsConfigSection>();
            BindSpawnSettings<SpawnSettingsConfigSection>();

            Container.BindInterfacesAndSelfTo<GameplaySettingsConfigValidator>().AsSingle();
        }

        private void BindPlayerSettings<TSettings>()
            where TSettings :
            IPlayerMovementSettingsData,
            IPlayerCollisionSettingsData,
            IKeyboardInputSettingsData,
            IPlayerInputRouterSettingsData,
            IMobileInputSettingsData
        {
            Container.Bind<IPlayerMovementSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IPlayerCollisionSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IKeyboardInputSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IPlayerInputRouterSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IMobileInputSettingsData>().To<TSettings>().FromResolve();
        }

        private void BindWeaponSettings<TSettings>() where TSettings : IBulletSettingsData, IPlayerLaserSettingsData
        {
            Container.Bind<IBulletSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IPlayerLaserSettingsData>().To<TSettings>().FromResolve();
        }

        private void BindEnemySettings<TSettings>() 
            where TSettings : IAsteroidSettingsData, IUfoSettingsData, IEnemyRewardSettingsData
        {
            Container.Bind<IAsteroidSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IUfoSettingsData>().To<TSettings>().FromResolve();
            Container.Bind<IEnemyRewardSettingsData>().To<TSettings>().FromResolve();
        }

        private void BindWorldSettings<TSettings>() where TSettings : IWorldSettingsData => 
            Container.Bind<IWorldSettingsData>().To<TSettings>().FromResolve();
        
        private void BindSpawnSettings<TSettings>() where TSettings : IEnemySpawnSettingsData => 
            Container.Bind<IEnemySpawnSettingsData>().To<TSettings>().FromResolve();
    }
}