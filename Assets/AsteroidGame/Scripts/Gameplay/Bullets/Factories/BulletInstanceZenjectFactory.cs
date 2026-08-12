using Zenject;
using AsteroidGame.Scripts.Domain.Bullets.Models;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Gameplay.Bullets.Contracts;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Factories
{
    public sealed class BulletInstanceZenjectFactory :
        PlaceholderFactory<BulletModel, CollisionBody, IBulletView, BulletInstance>
    {
    }
}