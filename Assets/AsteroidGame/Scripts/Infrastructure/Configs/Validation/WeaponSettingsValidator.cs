using System;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;
using AsteroidGame.Scripts.Domain.Player.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Validation
{
    public sealed class WeaponSettingsValidator
    {
        public void Validate(IBulletSettingsData bullets, IPlayerLaserSettingsData laser)
        {
            ValidateBullets(bullets);
            ValidateLaser(laser);
        }

        private void ValidateBullets(IBulletSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.PoolSize <= 0)
                throw new InvalidOperationException("Bullet pool size must be greater than zero.");

            if (settings.BulletSpeed <= 0f)
                throw new InvalidOperationException("Bullet speed must be greater than zero.");

            if (settings.BulletLifetimeSeconds <= 0f)
                throw new InvalidOperationException("Bullet lifetime seconds must be greater than zero.");

            if (settings.BulletRadius <= 0f)
                throw new InvalidOperationException("Bullet radius must be greater than zero.");

            if (settings.BulletShotsPerSecond <= 0f)
                throw new InvalidOperationException("Bullet shots per second must be greater than zero.");

            if (settings.BulletSpawnOffset < 0f)
                throw new InvalidOperationException("Bullet spawn offset cannot be negative.");

            if (settings.BulletVisibilityMargin < 0f)
                throw new InvalidOperationException("Bullet visibility margin cannot be negative.");
        }

        private void ValidateLaser(IPlayerLaserSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.PlayerMaxLaserCharges <= 0)
                throw new InvalidOperationException("Player max laser charges must be greater than zero.");

            if (settings.PlayerInitialLaserCharges < 0)
                throw new InvalidOperationException("Player initial laser charges cannot be negative.");

            if (settings.PlayerInitialLaserCharges > settings.PlayerMaxLaserCharges)
            {
                throw new InvalidOperationException(
                    "Player initial laser charges cannot be greater than max laser charges.");
            }

            if (settings.PlayerLaserRechargeSeconds <= 0f)
                throw new InvalidOperationException("Player laser recharge seconds must be greater than zero.");

            if (settings.PlayerLaserVisibleSeconds <= 0f)
                throw new InvalidOperationException("Player laser visible seconds must be greater than zero.");

            if (settings.PlayerLaserLength <= 0f)
                throw new InvalidOperationException("Player laser length must be greater than zero.");

            if (settings.PlayerLaserHitHalfWidth <= 0f)
                throw new InvalidOperationException("Player laser hit half width must be greater than zero.");

            if (settings.PlayerLaserVisualWidth <= 0f)
                throw new InvalidOperationException("Player laser visual width must be greater than zero.");
        }
    }
}