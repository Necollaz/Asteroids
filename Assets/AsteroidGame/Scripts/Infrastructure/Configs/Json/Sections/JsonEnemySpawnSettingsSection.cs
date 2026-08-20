using System;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.World;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections
{
    public sealed class JsonEnemySpawnSettingsSection : IEnemySpawnSettingsData
    {
        private readonly SpawnSettingsJson _settings;

        public JsonEnemySpawnSettingsSection(WorldSettingsJson world)
        {
            if (world == null)
                throw new ArgumentNullException(nameof(world));

            _settings = world.Spawning ?? throw new ArgumentNullException(nameof(world.Spawning));
        }

        public int AsteroidPoolSize => _settings.AsteroidPoolSize;
        public int MaxActiveAsteroids => _settings.MaxActiveAsteroids;
        public int UfoPoolSize => _settings.UfoPoolSize;
        public int MaxActiveUfo => _settings.MaxActiveUfo;
        public int AsteroidExplosionPoolSize => _settings.AsteroidExplosionPoolSize;
        public int UfoExplosionPoolSize => _settings.UfoExplosionPoolSize;
        public float AsteroidSpawnIntervalSeconds => _settings.AsteroidSpawnIntervalSeconds;
        public float AsteroidSpawnMargin => _settings.AsteroidSpawnMargin;
        public float UfoSpawnIntervalSeconds => _settings.UfoSpawnIntervalSeconds;
        public float UfoSpawnMargin => _settings.UfoSpawnMargin;
    }
}