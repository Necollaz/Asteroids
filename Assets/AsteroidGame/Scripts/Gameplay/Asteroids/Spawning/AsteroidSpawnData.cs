using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Spawning
{
    public readonly struct AsteroidSpawnData
    {
        public AsteroidSpawnData(Vector2D position, Vector2D direction)
        {
            Position = position;
            Direction = direction;
        }

        public Vector2D Position { get; }
        public Vector2D Direction { get; }
    }
}