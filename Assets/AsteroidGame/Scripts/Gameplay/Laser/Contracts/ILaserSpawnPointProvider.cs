using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Gameplay.Laser.Contracts
{
    public interface ILaserSpawnPointProvider
    {
        Vector2D Position { get; }
        Vector2D Direction { get; }
    }
}