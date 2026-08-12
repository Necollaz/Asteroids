using Zenject;
using AsteroidGame.Scripts.Domain.Asteroids.Models;
using AsteroidGame.Scripts.Domain.Enemies.Models;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Factories
{
    public sealed class AsteroidModelFactory : PlaceholderFactory<EnemyModel, AsteroidModel>
    {
    }
}