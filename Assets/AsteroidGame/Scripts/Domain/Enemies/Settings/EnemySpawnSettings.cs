using System;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;

namespace AsteroidGame.Scripts.Domain.Enemies.Settings
{
    public sealed class EnemySpawnSettings
    {
        private readonly IEnemySpawnSettingsData _data;
        
        public EnemySpawnSettings(IEnemySpawnSettingsData data) =>
            _data = data ?? throw new ArgumentNullException(nameof(data));
        
        public int AsteroidPoolSize => _data.AsteroidPoolSize;
        public int MaxActiveAsteroids => _data.MaxActiveAsteroids;
        public int UfoPoolSize => _data.UfoPoolSize;
        public int MaxActiveUfo => _data.MaxActiveUfo;
        public int AsteroidExplosionPoolSize => _data.AsteroidExplosionPoolSize;
        public int UfoExplosionPoolSize => _data.UfoExplosionPoolSize;
        public float AsteroidSpawnIntervalSeconds => _data.AsteroidSpawnIntervalSeconds;
        public float AsteroidSpawnMargin => _data.AsteroidSpawnMargin;
        public float UfoSpawnIntervalSeconds => _data.UfoSpawnIntervalSeconds;
        public float UfoSpawnMargin => _data.UfoSpawnMargin;
    }
}