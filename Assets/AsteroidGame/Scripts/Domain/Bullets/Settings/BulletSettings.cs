using System;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;

namespace AsteroidGame.Scripts.Domain.Bullets.Settings
{
    public sealed class BulletSettings
    {
        private readonly IBulletSettingsData _settingsData;

        public BulletSettings(IBulletSettingsData settingsData) =>
            _settingsData = settingsData ?? throw new ArgumentNullException(nameof(settingsData));

        public int PoolSize => _settingsData.PoolSize;
        public float Speed => _settingsData.BulletSpeed;
        public float LifetimeSeconds => _settingsData.BulletLifetimeSeconds;
        public float Radius => _settingsData.BulletRadius;
        public float ShotsPerSecond => _settingsData.BulletShotsPerSecond;
        public float SpawnOffset => _settingsData.BulletSpawnOffset;
        public float VisibilityMargin => _settingsData.BulletVisibilityMargin;
        public float FireCooldownSeconds => 1f / ShotsPerSecond;
    }
}