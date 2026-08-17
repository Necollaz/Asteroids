using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player
{
    [Serializable]
    public sealed class PlayerCollisionSettingsJson
    {
        public int MaxHealth;
        public float CollisionRadius;
        public float CollisionBounceSpeed;
        public float InvulnerabilitySeconds;
    }
}