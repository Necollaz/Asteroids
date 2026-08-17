using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player
{
    [Serializable]
    public sealed class PlayerBulletSettingsJson
    {
        public int PoolSize;
        public float Speed;
        public float LifetimeSeconds;
        public float Radius;
        public float ShotsPerSecond;
        public float SpawnOffset;
        public float VisibilityMargin;
    }
}