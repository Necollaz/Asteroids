using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Validation
{
    public sealed class GameplayJsonSettingsValidator
    {
        public void Validate(GameplayJsonSettings settings)
        {
            ValidatePlayer(settings);
            ValidateBullets(settings);
            ValidateLaser(settings);
            ValidateAsteroids(settings);
            ValidateUfo(settings);
            ValidateWorld(settings);
            ValidateMobileInput(settings);
            ValidateRewards(settings);
        }

        private void ValidatePlayer(GameplayJsonSettings settings)
        {
            if (settings.PlayerMaxHealth <= 0)
                throw new InvalidOperationException("player.collision.maxHealth must be greater than zero.");

            if (settings.PlayerAcceleration <= 0f)
                throw new InvalidOperationException("player.movement.acceleration must be greater than zero.");

            if (settings.PlayerTurnSpeed <= 0f)
                throw new InvalidOperationException("player.movement.turnSpeed must be greater than zero.");

            if (settings.PlayerMaxSpeed <= 0f)
                throw new InvalidOperationException("player.movement.maxSpeed must be greater than zero.");

            if (settings.PlayerLinearDamping < 0f)
                throw new InvalidOperationException("player.movement.linearDamping cannot be negative.");

            if (settings.PlayerCollisionRadius <= 0f)
                throw new InvalidOperationException("player.collision.collisionRadius must be greater than zero.");

            if (settings.PlayerCollisionBounceSpeed <= 0f)
                throw new InvalidOperationException("player.collision.collisionBounceSpeed must be greater than zero.");

            if (settings.PlayerInvulnerabilitySeconds <= 0f)
            {
                throw new InvalidOperationException(
                    "player.collision.invulnerabilitySeconds must be greater than zero.");
            }
        }

        private void ValidateBullets(GameplayJsonSettings settings)
        {
            if (settings.PoolSize <= 0)
                throw new InvalidOperationException("player.bullets.poolSize must be greater than zero.");

            if (settings.BulletSpeed <= 0f)
                throw new InvalidOperationException("player.bullets.speed must be greater than zero.");

            if (settings.BulletLifetimeSeconds <= 0f)
                throw new InvalidOperationException("player.bullets.lifetimeSeconds must be greater than zero.");

            if (settings.BulletRadius <= 0f)
                throw new InvalidOperationException("player.bullets.radius must be greater than zero.");

            if (settings.BulletShotsPerSecond <= 0f)
                throw new InvalidOperationException("player.bullets.shotsPerSecond must be greater than zero.");

            if (settings.BulletSpawnOffset < 0f)
                throw new InvalidOperationException("player.bullets.spawnOffset cannot be negative.");

            if (settings.BulletVisibilityMargin < 0f)
                throw new InvalidOperationException("player.bullets.visibilityMargin cannot be negative.");
        }

        private void ValidateLaser(GameplayJsonSettings settings)
        {
            if (settings.PlayerMaxLaserCharges <= 0)
                throw new InvalidOperationException("player.laser.maxCharges must be greater than zero.");

            if (settings.PlayerInitialLaserCharges < 0)
                throw new InvalidOperationException("player.laser.initialCharges cannot be negative.");

            if (settings.PlayerInitialLaserCharges > settings.PlayerMaxLaserCharges)
                throw new InvalidOperationException("player.laser.initialCharges cannot be greater than maxCharges.");

            if (settings.PlayerLaserRechargeSeconds <= 0f)
                throw new InvalidOperationException("player.laser.rechargeSeconds must be greater than zero.");

            if (settings.PlayerLaserVisibleSeconds <= 0f)
                throw new InvalidOperationException("player.laser.visibleSeconds must be greater than zero.");

            if (settings.PlayerLaserLength <= 0f)
                throw new InvalidOperationException("player.laser.length must be greater than zero.");

            if (settings.PlayerLaserHitHalfWidth <= 0f)
                throw new InvalidOperationException("player.laser.hitHalfWidth must be greater than zero.");

            if (settings.PlayerLaserVisualWidth <= 0f)
                throw new InvalidOperationException("player.laser.visualWidth must be greater than zero.");
        }

        private void ValidateAsteroids(GameplayJsonSettings settings)
        {
            if (settings.AsteroidPoolSize <= 0)
                throw new InvalidOperationException("world.spawning.asteroidPoolSize must be greater than zero.");

            if (settings.MaxActiveAsteroids <= 0)
                throw new InvalidOperationException("world.spawning.maxActiveAsteroids must be greater than zero.");

            if (settings.AsteroidSpawnIntervalSeconds <= 0f)
            {
                throw new InvalidOperationException(
                    "world.spawning.asteroidSpawnIntervalSeconds must be greater than zero.");
            }

            if (settings.AsteroidSpawnMargin < 0f)
                throw new InvalidOperationException("world.spawning.asteroidSpawnMargin cannot be negative.");

            if (settings.LargeAsteroidRadius <= 0f ||
                settings.MediumAsteroidRadius <= 0f ||
                settings.SmallAsteroidRadius <= 0f)
            {
                throw new InvalidOperationException("enemies.asteroids radii must be greater than zero.");
            }

            if (settings.LargeAsteroidSpeed <= 0f)
                throw new InvalidOperationException("enemies.asteroids.largeSpeed must be greater than zero.");

            if (settings.MediumAsteroidSpeed <= settings.LargeAsteroidSpeed)
                throw new InvalidOperationException("enemies.asteroids.mediumSpeed must be greater than largeSpeed.");

            if (settings.SmallAsteroidSpeed <= settings.MediumAsteroidSpeed)
                throw new InvalidOperationException("enemies.asteroids.smallSpeed must be greater than mediumSpeed.");

            if (settings.MediumFragmentsPerLarge <= 0)
            {
                throw new InvalidOperationException(
                    "enemies.asteroids.mediumFragmentsPerLarge must be greater than zero.");
            }

            if (settings.SmallFragmentsPerMedium <= 0)
            {
                throw new InvalidOperationException(
                    "enemies.asteroids.smallFragmentsPerMedium must be greater than zero.");
            }

            if (settings.AsteroidSpeedReturnRate <= 0f)
                throw new InvalidOperationException("enemies.asteroids.speedReturnRate must be greater than zero.");
        }

        private void ValidateUfo(GameplayJsonSettings settings)
        {
            if (settings.UfoPoolSize <= 0)
                throw new InvalidOperationException("world.spawning.ufoPoolSize must be greater than zero.");

            if (settings.MaxActiveUfo <= 0)
                throw new InvalidOperationException("world.spawning.maxActiveUfo must be greater than zero.");

            if (settings.UfoSpawnIntervalSeconds <= 0f)
            {
                throw new InvalidOperationException(
                    "world.spawning.ufoSpawnIntervalSeconds must be greater than zero.");
            }

            if (settings.UfoSpawnMargin < 0f)
                throw new InvalidOperationException("world.spawning.ufoSpawnMargin cannot be negative.");

            if (settings.UfoSpeed <= 0f)
                throw new InvalidOperationException("enemies.ufo.speed must be greater than zero.");

            if (settings.UfoCollisionRadius <= 0f)
                throw new InvalidOperationException("enemies.ufo.collisionRadius must be greater than zero.");

            if (settings.UfoMaxTiltDegrees < 0f)
                throw new InvalidOperationException("enemies.ufo.maxTiltDegrees cannot be negative.");

            if (settings.UfoKnockbackSeconds <= 0f)
                throw new InvalidOperationException("enemies.ufo.knockbackSeconds must be greater than zero.");

            if (settings.UfoKnockbackDamping < 0f)
                throw new InvalidOperationException("enemies.ufo.knockbackDamping cannot be negative.");
        }

        private void ValidateWorld(GameplayJsonSettings settings)
        {
            if (settings.WorldWidth <= 0f)
                throw new InvalidOperationException("world.world.width must be greater than zero.");

            if (settings.WorldHeight <= 0f)
                throw new InvalidOperationException("world.world.height must be greater than zero.");
        }

        private void ValidateMobileInput(GameplayJsonSettings settings)
        {
            if (settings.MobileTurnLeftValue < -1f || settings.MobileTurnLeftValue > 1f)
                throw new InvalidOperationException("player.input.mobileTurnLeftValue must be between -1 and 1.");

            if (settings.MobileTurnRightValue < -1f || settings.MobileTurnRightValue > 1f)
                throw new InvalidOperationException("player.input.mobileTurnRightValue must be between -1 and 1.");

            if (settings.MobileTurnLeftValue == 0f)
                throw new InvalidOperationException("player.input.mobileTurnLeftValue cannot be zero.");

            if (settings.MobileTurnRightValue == 0f)
                throw new InvalidOperationException("player.input.mobileTurnRightValue cannot be zero.");
        }

        private void ValidateRewards(GameplayJsonSettings settings)
        {
            if (settings.LargeAsteroidReward < 0 ||
                settings.MediumAsteroidReward < 0 ||
                settings.SmallAsteroidReward < 0 ||
                settings.UfoReward < 0)
            {
                throw new InvalidOperationException("enemies.rewards values cannot be negative.");
            }
        }
    }
}