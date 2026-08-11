using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Player.Models
{
    public readonly struct PlayerSnapshot
    {
        public PlayerSnapshot(
            Vector2D position,
            Vector2D velocity,
            float rotationDegrees,
            int currentHealth,
            int maxHealth,
            bool isInvulnerable,
            int laserCharges,
            int maxLaserCharges)
        {
            Position = position;
            Velocity = velocity;
            RotationDegrees = rotationDegrees;
            Speed = velocity.Magnitude;
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
            IsInvulnerable = isInvulnerable;
            LaserCharges = laserCharges;
            MaxLaserCharges = maxLaserCharges;
        }

        public Vector2D Position { get; }
        public Vector2D Velocity { get; }
        public float RotationDegrees { get; }
        public float Speed { get; }
        public int CurrentHealth { get; }
        public int MaxHealth { get; }
        public int LaserCharges { get; }
        public int MaxLaserCharges { get; }
        public bool IsInvulnerable { get; }
    }
}