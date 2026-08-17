using System;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections
{
    public sealed class JsonWeaponSettingsSection : IBulletSettingsData, IPlayerLaserSettingsData
    {
        private readonly PlayerSettingsJson _settings;

        public JsonWeaponSettingsSection(PlayerSettingsJson settings) =>
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        public int PoolSize => _settings.Bullets.PoolSize;
        public int PlayerMaxLaserCharges => _settings.Laser.MaxCharges;
        public int PlayerInitialLaserCharges => _settings.Laser.InitialCharges;
        public float BulletSpeed => _settings.Bullets.Speed;
        public float BulletLifetimeSeconds => _settings.Bullets.LifetimeSeconds;
        public float BulletRadius => _settings.Bullets.Radius;
        public float BulletShotsPerSecond => _settings.Bullets.ShotsPerSecond;
        public float BulletSpawnOffset => _settings.Bullets.SpawnOffset;
        public float BulletVisibilityMargin => _settings.Bullets.VisibilityMargin;
        public float PlayerLaserRechargeSeconds => _settings.Laser.RechargeSeconds;
        public float PlayerLaserVisibleSeconds => _settings.Laser.VisibleSeconds;
        public float PlayerLaserLength => _settings.Laser.Length;
        public float PlayerLaserHitHalfWidth => _settings.Laser.HitHalfWidth;
        public float PlayerLaserVisualWidth => _settings.Laser.VisualWidth;
    }
}