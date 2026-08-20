using System;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Validation
{
    public sealed class SpawnSettingsValidator
    {
        public void Validate(IEnemySpawnSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.AsteroidPoolSize <= 0)
                throw new InvalidOperationException("Asteroid pool size must be greater than zero.");

            if (settings.MaxActiveAsteroids <= 0)
                throw new InvalidOperationException("Max active asteroids must be greater than zero.");

            if (settings.UfoPoolSize <= 0)
                throw new InvalidOperationException("UFO pool size must be greater than zero.");

            if (settings.MaxActiveUfo <= 0)
                throw new InvalidOperationException("Max active UFO must be greater than zero.");

            if (settings.AsteroidExplosionPoolSize <= 0)
                throw new InvalidOperationException("Asteroid explosion pool size must be greater than zero.");

            if (settings.UfoExplosionPoolSize <= 0)
                throw new InvalidOperationException("UFO explosion pool size must be greater than zero.");

            if (settings.AsteroidSpawnIntervalSeconds <= 0f)
                throw new InvalidOperationException("Asteroid spawn interval seconds must be greater than zero.");

            if (settings.AsteroidSpawnMargin < 0f)
                throw new InvalidOperationException("Asteroid spawn margin cannot be negative.");

            if (settings.UfoSpawnIntervalSeconds <= 0f)
                throw new InvalidOperationException("UFO spawn interval seconds must be greater than zero.");

            if (settings.UfoSpawnMargin < 0f)
                throw new InvalidOperationException("UFO spawn margin cannot be negative.");
        }
    }
}