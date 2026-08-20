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
            
            Speed = settingsData.UfoSpeed;
            CollisionRadius = settingsData.UfoCollisionRadius;
            MaxTiltDegrees = settingsData.UfoMaxTiltDegrees;
            KnockbackSeconds = settingsData.UfoKnockbackSeconds;
            KnockbackDamping = settingsData.UfoKnockbackDamping;
        }
        
        public float Speed { get; }
        public float CollisionRadius { get; }
        public float MaxTiltDegrees { get; }
        public float KnockbackSeconds { get; }
        public float KnockbackDamping { get; }
    }
}