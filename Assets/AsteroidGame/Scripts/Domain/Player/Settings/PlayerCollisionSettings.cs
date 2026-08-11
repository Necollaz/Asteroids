using System;
using AsteroidGame.Scripts.Domain.Player.Contracts;

namespace AsteroidGame.Scripts.Domain.Player.Settings
{
    public sealed class PlayerCollisionSettings
    {
        public PlayerCollisionSettings(IPlayerCollisionSettingsData settingsData)
        {
            if (settingsData.PlayerMaxHealth <= 0)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerMaxHealth));
            
            if (settingsData.PlayerCollisionRadius <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerCollisionRadius));
            
            if (settingsData.PlayerCollisionBounceSpeed <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerCollisionBounceSpeed));
            
            if (settingsData.PlayerInvulnerabilitySeconds <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerInvulnerabilitySeconds));
            
            MaxHealth = settingsData.PlayerMaxHealth;
            CollisionRadius = settingsData.PlayerCollisionRadius;
            CollisionBounceSpeed = settingsData.PlayerCollisionBounceSpeed;
            InvulnerabilitySeconds = settingsData.PlayerInvulnerabilitySeconds;
        }
        
        public int MaxHealth { get; }
        public float CollisionRadius { get; }
        public float CollisionBounceSpeed { get; }
        public float InvulnerabilitySeconds { get; }
    }
}