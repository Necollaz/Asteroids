using Zenject;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Factories
{
    public sealed class EnemyModelFactory : PlaceholderFactory<EnemyType, Body2D, EnemyModel>
    {
    }
}