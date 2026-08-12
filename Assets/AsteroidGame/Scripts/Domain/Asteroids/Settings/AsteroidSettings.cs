using System;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Types;

namespace AsteroidGame.Scripts.Domain.Asteroids.Settings
{
    public sealed class AsteroidSettings
    {
        private readonly IAsteroidSettingsData _settingsData;
        
        public AsteroidSettings(IAsteroidSettingsData settingsData) =>
            _settingsData = settingsData ?? throw new ArgumentNullException(nameof(settingsData));
        
        public int PoolSize => _settingsData.AsteroidPoolSize;
        public int MaxActiveAsteroids => _settingsData.MaxActiveAsteroids;
        public float SpawnIntervalSeconds => _settingsData.AsteroidSpawnIntervalSeconds;
        public float SpawnMargin => _settingsData.AsteroidSpawnMargin;
        public float SpeedReturnRate => _settingsData.AsteroidSpeedReturnRate;

        public float GetRadius(EnemyType type)
        {
            return type switch
            {
                EnemyType.LargeAsteroid => _settingsData.LargeAsteroidRadius,
                EnemyType.MediumAsteroid => _settingsData.MediumAsteroidRadius,
                EnemyType.SmallAsteroid => _settingsData.SmallAsteroidRadius,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public float GetSpeed(EnemyType type)
        {
            return type switch
            {
                EnemyType.LargeAsteroid => _settingsData.LargeAsteroidSpeed,
                EnemyType.MediumAsteroid => _settingsData.MediumAsteroidSpeed,
                EnemyType.SmallAsteroid => _settingsData.SmallAsteroidSpeed,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }

        public bool TryGetFragmentType(EnemyType type, out EnemyType fragmentType, out int count)
        {
            fragmentType = type switch
            {
                EnemyType.LargeAsteroid => EnemyType.MediumAsteroid,
                EnemyType.MediumAsteroid => EnemyType.SmallAsteroid,
                _ => default
            };

            count = type switch
            {
                EnemyType.LargeAsteroid => _settingsData.MediumFragmentsPerLarge,
                EnemyType.MediumAsteroid => _settingsData.SmallFragmentsPerMedium,
                _ => 0
            };
            
            return count > 0;
        }
    }
}