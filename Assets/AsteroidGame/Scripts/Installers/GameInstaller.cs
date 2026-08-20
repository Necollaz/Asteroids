using AsteroidGame.Scripts.Infrastructure.Ads.Settings;
using AsteroidGame.Scripts.Infrastructure.Analytics.Settings;
using UnityEngine;
using Zenject;
using AsteroidGame.Scripts.Infrastructure.Configs;
using AsteroidGame.Scripts.Installers.Core;
using AsteroidGame.Scripts.Installers.Enemies;
using AsteroidGame.Scripts.Installers.Player;
using AsteroidGame.Scripts.Installers.Presentation;
using AsteroidGame.Scripts.Installers.SceneReferences;
using AsteroidGame.Scripts.Installers.Weapons;
using AsteroidGame.Scripts.Presentation.Asteroids;
using AsteroidGame.Scripts.Presentation.Asteroids.Effects;
using AsteroidGame.Scripts.Presentation.Bullets;
using AsteroidGame.Scripts.Presentation.Ufo;
using AsteroidGame.Scripts.Presentation.Ufo.Effects;

namespace AsteroidGame.Scripts.Installers
{
    public sealed class GameInstaller : MonoInstaller, IGameplaySceneReferences
    {
        [SerializeField] private GameplaySettingsConfig _gameplaySettingsConfig;
        [SerializeField] private AdsSettingsConfig _adsSettingsConfig;
        [SerializeField] private AnalyticsSettingsConfig _analyticsSettingsConfig;
        [SerializeField] private BulletView _bulletPrefab;
        [SerializeField] private Transform _container;
        
        [Header("Asteroids")]
        [SerializeField] private AsteroidView _largeAsteroidPrefab;
        [SerializeField] private AsteroidView _mediumAsteroidPrefab;
        [SerializeField] private AsteroidView _smallAsteroidPrefab;
        [SerializeField] private AsteroidExplosionView _asteroidExplosionPrefab;
        
        [Header("UFO")]
        [SerializeField] private UfoView _ufoPrefab;
        [SerializeField] private UfoExplosionView _ufoExplosionPrefab;
        
        public GameplaySettingsConfig GameplaySettingsConfig => _gameplaySettingsConfig;
        public AdsSettingsConfig AdsSettingsConfig => _adsSettingsConfig;
        public AnalyticsSettingsConfig AnalyticsSettingsConfig => _analyticsSettingsConfig;
        public BulletView BulletPrefab => _bulletPrefab;
        public Transform GameplayRoot => _container;
        public AsteroidView LargeAsteroidPrefab => _largeAsteroidPrefab;
        public AsteroidView MediumAsteroidPrefab => _mediumAsteroidPrefab;
        public AsteroidView SmallAsteroidPrefab => _smallAsteroidPrefab;
        public AsteroidExplosionView AsteroidExplosionPrefab => _asteroidExplosionPrefab;
        public UfoView UfoPrefab => _ufoPrefab;
        public UfoExplosionView UfoExplosionPrefab => _ufoExplosionPrefab;

        public override void InstallBindings()
        {
            Container.Bind<IGameplaySceneReferences>().FromInstance(this).AsSingle();

            GameSignalsInstaller.Install(Container);
            GameSettingsInstaller.Install(Container, this);
            PhysicsBindingsInstaller.Install(Container);
            WorldBindingsInstaller.Install(Container);
            GameStateBindingsInstaller.Install(Container);
            CollisionBindingsInstaller.Install(Container);
            PlayerBindingsInstaller.Install(Container);
            BulletBindingsInstaller.Install(Container, this);
            EnemyRegistryBindingsInstaller.Install(Container);
            AsteroidBindingsInstaller.Install(Container, this);
            UfoBindingsInstaller.Install(Container, this);
            EffectsBindingsInstaller.Install(Container, this);
            LaserBindingsInstaller.Install(Container);
            ScoreBindingsInstaller.Install(Container);
            GameplayPresentationInstaller.Install(Container);
            UiBindingsInstaller.Install(Container);
            InfrastructureBindingsInstaller.Install(Container, this);
            InputBindingsInstaller.Install(Container);
            ExecutionOrderInstaller.Install(Container);
        }
    }
}