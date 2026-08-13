using System;
using AsteroidGame.Scripts.Domain.Ufo.Contracts;

namespace AsteroidGame.Scripts.Domain.Ufo.Settings
{
    public sealed class UfoSettings
    {
        public UfoSettings(IUfoSettingsData settingsData)
        {
            if (settingsData == null)
                throw new ArgumentNullException(nameof(settingsData));
            
            if (settingsData.UfoPoolSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(settingsData.UfoPoolSize));

            if (settingsData.MaxActiveUfo <= 0)
                throw new ArgumentOutOfRangeException(nameof(settingsData.MaxActiveUfo));

            if (settingsData.UfoSpawnIntervalSeconds <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.UfoSpawnIntervalSeconds));

            if (settingsData.UfoSpawnMargin < 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.UfoSpawnMargin));

            if (settingsData.UfoSpeed <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.UfoSpeed));

            if (settingsData.UfoCollisionRadius <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.UfoCollisionRadius));
            
            if (settingsData.UfoMaxTiltDegrees < 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.UfoMaxTiltDegrees));
            
            if (settingsData.UfoKnockbackSeconds <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.UfoKnockbackSeconds));

            if (settingsData.UfoKnockbackDamping < 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.UfoKnockbackDamping));
            
            PoolSize = settingsData.UfoPoolSize;
            MaxActiveUfo = settingsData.MaxActiveUfo;
            SpawnIntervalSeconds = settingsData.UfoSpawnIntervalSeconds;
            SpawnMargin = settingsData.UfoSpawnMargin;
            Speed = settingsData.UfoSpeed;
            CollisionRadius = settingsData.UfoCollisionRadius;
            MaxTiltDegrees = settingsData.UfoMaxTiltDegrees;
            KnockbackSeconds = settingsData.UfoKnockbackSeconds;
            KnockbackDamping = settingsData.UfoKnockbackDamping;
        }
        
        public int PoolSize { get; }
        public int MaxActiveUfo { get; }
        public float SpawnIntervalSeconds { get; }
        public float SpawnMargin { get; }
        public float Speed { get; }
        public float CollisionRadius { get; }
        public float MaxTiltDegrees { get; }
        public float KnockbackSeconds { get; }
        public float KnockbackDamping { get; }
    }
}