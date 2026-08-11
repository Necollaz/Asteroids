using Zenject;
using AsteroidGame.Scripts.Domain.Bullets.Models;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Factories
{
    public sealed class BulletModelFactory : PlaceholderFactory<Body2D, BulletModel>
    {
    }
}