using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Bullets.Models
{
    public readonly struct BulletSnapshot
    {
        public BulletSnapshot(Vector2D position, float rotationDegrees)
        {
            Position = position;
            RotationDegrees = rotationDegrees;
        }
        
        public Vector2D Position { get; }
        public float RotationDegrees { get; }
    }
}