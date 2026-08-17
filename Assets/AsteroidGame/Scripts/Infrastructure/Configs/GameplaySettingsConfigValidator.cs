using System;
using UnityEngine;
using Zenject;

namespace AsteroidGame.Scripts.Infrastructure.Configs
{
    public sealed class GameplaySettingsConfigValidator : IInitializable
    {
        private readonly GameplaySettingsConfig _config;

        public GameplaySettingsConfigValidator(GameplaySettingsConfig config) => _config = config;

        void IInitializable.Initialize()
        {
            if (_config.SettingsSource != GameplaySettingsSource.ScriptableObject)
                return;

            ValidatePlayerMovement();
            ValidatePlayerCollision();
            ValidatePlayerLaser();
            ValidateKeyboardInput();
            ValidateMobileInput();
            ValidateBullets();
            ValidateAsteroids();
            ValidateUfo();
            ValidateWorld();
            ValidateRewards();
        }

        private void ValidatePlayerMovement()
        {
            if (_config.PlayerAcceleration <= 0f)
                throw new InvalidOperationException("Player acceleration must be greater than zero.");

            if (_config.PlayerTurnSpeed <= 0f)
                throw new InvalidOperationException("Player turn speed must be greater than zero.");

            if (_config.PlayerMaxSpeed <= 0f)
                throw new InvalidOperationException("Player max speed must be greater than zero.");

            if (_config.PlayerLinearDamping < 0f)
                throw new InvalidOperationException("Player linear damping cannot be negative.");
        }

        private void ValidatePlayerCollision()
        {
            if (_config.PlayerMaxHealth <= 0)
                throw new InvalidOperationException("Player max health must be greater than zero.");

            if (_config.PlayerCollisionRadius <= 0f)
                throw new InvalidOperationException("Player collision radius must be greater than zero.");

            if (_config.PlayerCollisionBounceSpeed <= 0f)
                throw new InvalidOperationException("Player collision bounce speed must be greater than zero.");

            if (_config.PlayerInvulnerabilitySeconds <= 0f)
                throw new InvalidOperationException("Player invulnerability seconds must be greater than zero.");
        }

        private void ValidatePlayerLaser()
        {
            if (_config.PlayerMaxLaserCharges <= 0)
                throw new InvalidOperationException("Player max laser charges must be greater than zero.");

            if (_config.PlayerInitialLaserCharges < 0)
                throw new InvalidOperationException("Player initial laser charges cannot be negative.");

            if (_config.PlayerInitialLaserCharges > _config.PlayerMaxLaserCharges)
            {
                throw new InvalidOperationException(
                    "Player initial laser charges cannot be greater than max laser charges.");
            }

            if (_config.PlayerLaserRechargeSeconds <= 0f)
                throw new InvalidOperationException("Player laser recharge seconds must be greater than zero.");

            if (_config.PlayerLaserVisibleSeconds <= 0f)
                throw new InvalidOperationException("Player laser visible seconds must be greater than zero.");

            if (_config.PlayerLaserLength <= 0f)
                throw new InvalidOperationException("Player laser length must be greater than zero.");

            if (_config.PlayerLaserHitHalfWidth <= 0f)
                throw new InvalidOperationException("Player laser hit half width must be greater than zero.");

            if (_config.PlayerLaserVisualWidth <= 0f)
                throw new InvalidOperationException("Player laser visual width must be greater than zero.");
        }

        private void ValidateBullets()
        {
            if (_config.PoolSize <= 0)
                throw new InvalidOperationException("Bullet pool size must be greater than zero.");

            if (_config.BulletSpeed <= 0f)
                throw new InvalidOperationException("Bullet speed must be greater than zero.");

            if (_config.BulletLifetimeSeconds <= 0f)
                throw new InvalidOperationException("Bullet lifetime seconds must be greater than zero.");

            if (_config.BulletRadius <= 0f)
                throw new InvalidOperationException("Bullet radius must be greater than zero.");

            if (_config.BulletShotsPerSecond <= 0f)
                throw new InvalidOperationException("Bullet shots per second must be greater than zero.");

            if (_config.BulletSpawnOffset < 0f)
                throw new InvalidOperationException("Bullet spawn offset cannot be negative.");

            if (_config.BulletVisibilityMargin < 0f)
                throw new InvalidOperationException("Bullet visibility margin cannot be negative.");
        }

        private void ValidateAsteroids()
        {
            if (_config.AsteroidPoolSize <= 0)
                throw new InvalidOperationException("Asteroid pool size must be greater than zero.");

            if (_config.MaxActiveAsteroids <= 0)
                throw new InvalidOperationException("Max active asteroids must be greater than zero.");

            if (_config.AsteroidSpawnIntervalSeconds <= 0f)
                throw new InvalidOperationException("Asteroid spawn interval seconds must be greater than zero.");

            if (_config.AsteroidSpawnMargin < 0f)
                throw new InvalidOperationException("Asteroid spawn margin cannot be negative.");

            if (_config.LargeAsteroidRadius <= 0f ||
                _config.MediumAsteroidRadius <= 0f ||
                _config.SmallAsteroidRadius <= 0f)
            {
                throw new InvalidOperationException("Asteroid radii must be greater than zero.");
            }

            if (_config.LargeAsteroidSpeed <= 0f)
                throw new InvalidOperationException("Large asteroid speed must be greater than zero.");

            if (_config.MediumAsteroidSpeed <= _config.LargeAsteroidSpeed)
                throw new InvalidOperationException("Medium asteroid speed must be greater than large asteroid speed.");

            if (_config.SmallAsteroidSpeed <= _config.MediumAsteroidSpeed)
                throw new InvalidOperationException("Small asteroid speed must be greater than medium asteroid speed.");

            if (_config.MediumFragmentsPerLarge <= 0)
                throw new InvalidOperationException("Medium fragments per large must be greater than zero.");

            if (_config.SmallFragmentsPerMedium <= 0)
                throw new InvalidOperationException("Small fragments per medium must be greater than zero.");

            if (_config.AsteroidSpeedReturnRate <= 0f)
                throw new InvalidOperationException("Asteroid speed return rate must be greater than zero.");
        }

        private void ValidateUfo()
        {
            if (_config.UfoPoolSize <= 0)
                throw new InvalidOperationException("UFO pool size must be greater than zero.");

            if (_config.MaxActiveUfo <= 0)
                throw new InvalidOperationException("Max active UFO must be greater than zero.");

            if (_config.UfoSpawnIntervalSeconds <= 0f)
                throw new InvalidOperationException("UFO spawn interval seconds must be greater than zero.");

            if (_config.UfoSpawnMargin < 0f)
                throw new InvalidOperationException("UFO spawn margin cannot be negative.");

            if (_config.UfoSpeed <= 0f)
                throw new InvalidOperationException("UFO speed must be greater than zero.");

            if (_config.UfoCollisionRadius <= 0f)
                throw new InvalidOperationException("UFO collision radius must be greater than zero.");

            if (_config.UfoMaxTiltDegrees < 0f)
                throw new InvalidOperationException("UFO max tilt degrees cannot be negative.");

            if (_config.UfoKnockbackSeconds <= 0f)
                throw new InvalidOperationException("UFO knockback seconds must be greater than zero.");

            if (_config.UfoKnockbackDamping < 0f)
                throw new InvalidOperationException("UFO knockback damping cannot be negative.");
        }

        private void ValidateWorld()
        {
            if (_config.WorldWidth <= 0f)
                throw new InvalidOperationException("World width must be greater than zero.");

            if (_config.WorldHeight <= 0f)
                throw new InvalidOperationException("World height must be greater than zero.");
        }

        private void ValidateRewards()
        {
            if (_config.LargeAsteroidReward < 0 ||
                _config.MediumAsteroidReward < 0 ||
                _config.SmallAsteroidReward < 0 ||
                _config.UfoReward < 0)
            {
                throw new InvalidOperationException("Enemy rewards cannot be negative.");
            }
        }

        private void ValidateKeyboardInput()
        {
            ValidateKey(_config.TurnLeftKey, nameof(_config.TurnLeftKey));
            ValidateKey(_config.TurnRightKey, nameof(_config.TurnRightKey));
            ValidateKey(_config.ThrustKey, nameof(_config.ThrustKey));
            ValidateKey(_config.AlternativeTurnLeftKey, nameof(_config.AlternativeTurnLeftKey));
            ValidateKey(_config.AlternativeTurnRightKey, nameof(_config.AlternativeTurnRightKey));
            ValidateKey(_config.AlternativeThrustKey, nameof(_config.AlternativeThrustKey));
            ValidateKey(_config.FireBulletKey, nameof(_config.FireBulletKey));
            ValidateKey(_config.AlternativeFireBulletKey, nameof(_config.AlternativeFireBulletKey));
            ValidateKey(_config.FireLaserKey, nameof(_config.FireLaserKey));
            ValidateKey(_config.AlternativeFireLaserKey, nameof(_config.AlternativeFireLaserKey));
        }

        private void ValidateMobileInput()
        {
            if (_config.MobileTurnLeftValue < -1f || _config.MobileTurnLeftValue > 1f)
                throw new InvalidOperationException("Mobile turn left value must be between -1 and 1.");

            if (_config.MobileTurnRightValue < -1f || _config.MobileTurnRightValue > 1f)
                throw new InvalidOperationException("Mobile turn right value must be between -1 and 1.");

            if (_config.MobileTurnLeftValue == 0f)
                throw new InvalidOperationException("Mobile turn left value cannot be zero.");

            if (_config.MobileTurnRightValue == 0f)
                throw new InvalidOperationException("Mobile turn right value cannot be zero.");
        }

        private void ValidateKey(KeyCode keyCode, string propertyName)
        {
            if (keyCode == KeyCode.None)
                throw new InvalidOperationException($"{propertyName} must be assigned.");
        }
    }
}