using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Types;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Contracts
{
    public interface IAsteroidViewFactory
    {
        IAsteroidView Create(EnemyType type);
    }
}