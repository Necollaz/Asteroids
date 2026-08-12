using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Gameplay.Laser.Models
{
    public readonly struct PlayerLaserBeamSegment
    {
        public PlayerLaserBeamSegment(Vector2D startPosition, Vector2D endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public Vector2D StartPosition { get; }
        public Vector2D EndPosition { get; }
    }
}