using AsteroidGame.Scripts.Domain.Physics;

namespace AsteroidGame.Scripts.Domain.Player
{
    public readonly struct PlayerSnapshot
    {
        public PlayerSnapshot(Vector2D position, Vector2D velocity, float rotationDegrees)
        {
            Position = position;
            Velocity = velocity;
            RotationDegrees = rotationDegrees;
            Speed = velocity.Magnitude;
        }
        
        public Vector2D Position { get; }
        public Vector2D Velocity { get; }
        public float RotationDegrees { get; }
        public float Speed { get; }
    }
}