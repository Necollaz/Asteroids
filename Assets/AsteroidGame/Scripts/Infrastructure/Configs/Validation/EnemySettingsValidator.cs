using System;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Ufo.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Validation
{
    public sealed class EnemySettingsValidator
    {
        public void Validate(IAsteroidSettingsData asteroids, IUfoSettingsData ufo, IEnemyRewardSettingsData rewards)
        {
            ValidateAsteroids(asteroids);
            ValidateUfo(ufo);
            ValidateRewards(rewards);
        }

        private void ValidateAsteroids(IAsteroidSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.AsteroidPoolSize <= 0)
                throw new InvalidOperationException("Asteroid pool size must be greater than zero.");

            if (settings.MaxActiveAsteroids <= 0)
                throw new InvalidOperationException("Max active asteroids must be greater than zero.");

            if (settings.AsteroidSpawnIntervalSeconds <= 0f)
                throw new InvalidOperationException("Asteroid spawn interval seconds must be greater than zero.");

            if (settings.AsteroidSpawnMargin < 0f)
                throw new InvalidOperationException("Asteroid spawn margin cannot be negative.");

            if (settings.LargeAsteroidRadius <= 0f ||
                settings.MediumAsteroidRadius <= 0f ||
                settings.SmallAsteroidRadius <= 0f)
                throw new InvalidOperationException("Asteroid radii must be greater than zero.");

            if (settings.LargeAsteroidSpeed <= 0f)
                throw new InvalidOperationException("Large asteroid speed must be greater than zero.");

            if (settings.MediumAsteroidSpeed <= settings.LargeAsteroidSpeed)
                throw new InvalidOperationException("Medium asteroid speed must be greater than large asteroid speed.");

            if (settings.SmallAsteroidSpeed <= settings.MediumAsteroidSpeed)
                throw new InvalidOperationException("Small asteroid speed must be greater than medium asteroid speed.");

            if (settings.MediumFragmentsPerLarge <= 0)
                throw new InvalidOperationException("Medium fragments per large must be greater than zero.");

            if (settings.SmallFragmentsPerMedium <= 0)
                throw new InvalidOperationException("Small fragments per medium must be greater than zero.");

            if (settings.AsteroidSpeedReturnRate <= 0f)
                throw new InvalidOperationException("Asteroid speed return rate must be greater than zero.");
        }

        private void ValidateUfo(IUfoSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.UfoPoolSize <= 0)
                throw new InvalidOperationException("UFO pool size must be greater than zero.");

            if (settings.MaxActiveUfo <= 0)
                throw new InvalidOperationException("Max active UFO must be greater than zero.");

            if (settings.UfoSpawnIntervalSeconds <= 0f)
                throw new InvalidOperationException("UFO spawn interval seconds must be greater than zero.");

            if (settings.UfoSpawnMargin < 0f)
                throw new InvalidOperationException("UFO spawn margin cannot be negative.");

            if (settings.UfoSpeed <= 0f)
                throw new InvalidOperationException("UFO speed must be greater than zero.");

            if (settings.UfoCollisionRadius <= 0f)
                throw new InvalidOperationException("UFO collision radius must be greater than zero.");

            if (settings.UfoMaxTiltDegrees < 0f)
                throw new InvalidOperationException("UFO max tilt degrees cannot be negative.");

            if (settings.UfoKnockbackSeconds <= 0f)
                throw new InvalidOperationException("UFO knockback seconds must be greater than zero.");

            if (settings.UfoKnockbackDamping < 0f)
                throw new InvalidOperationException("UFO knockback damping cannot be negative.");
        }

        private void ValidateRewards(IEnemyRewardSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.LargeAsteroidReward < 0 ||
                settings.MediumAsteroidReward < 0 ||
                settings.SmallAsteroidReward < 0 ||
                settings.UfoReward < 0)
                throw new InvalidOperationException("Enemy rewards cannot be negative.");
        }
    }
}