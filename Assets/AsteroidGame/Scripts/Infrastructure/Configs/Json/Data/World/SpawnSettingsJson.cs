using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.World
{
    [Serializable]
    public sealed class SpawnSettingsJson
    {
        public int AsteroidPoolSize;
        public int MaxActiveAsteroids;
        public int UfoPoolSize;
        public int MaxActiveUfo;
        public float AsteroidSpawnIntervalSeconds;
        public float AsteroidSpawnMargin;
        public float UfoSpawnIntervalSeconds;
        public float UfoSpawnMargin;
    }
}