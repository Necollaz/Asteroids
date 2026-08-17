using UnityEngine;
using AsteroidGame.Scripts.Infrastructure.Ads.Settings;
using AsteroidGame.Scripts.Infrastructure.Analytics.Settings;
using AsteroidGame.Scripts.Infrastructure.Configs;
using AsteroidGame.Scripts.Presentation.Asteroids;
using AsteroidGame.Scripts.Presentation.Asteroids.Effects;
using AsteroidGame.Scripts.Presentation.Bullets;
using AsteroidGame.Scripts.Presentation.Ufo;
using AsteroidGame.Scripts.Presentation.Ufo.Effects;

namespace AsteroidGame.Scripts.Installers.SceneReferences
{
    public interface IGameplaySceneReferences
    {
        GameplaySettingsConfig GameplaySettingsConfig { get; }
        AdsSettingsConfig AdsSettingsConfig { get; }
        AnalyticsSettingsConfig AnalyticsSettingsConfig { get; }
        BulletView BulletPrefab { get; }
        AsteroidView LargeAsteroidPrefab { get; }
        AsteroidView MediumAsteroidPrefab { get; }
        AsteroidView SmallAsteroidPrefab { get; }
        AsteroidExplosionView AsteroidExplosionPrefab { get; }
        UfoView UfoPrefab { get; }
        UfoExplosionView UfoExplosionPrefab { get; }
        Transform GameplayRoot { get; }
    }
}