using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy
{
    [Serializable]
    public sealed class UfoSettingsJson
    {
        public float Speed;
        public float CollisionRadius;
        public float MaxTiltDegrees;
        public float KnockbackSeconds;
        public float KnockbackDamping;
    }
}