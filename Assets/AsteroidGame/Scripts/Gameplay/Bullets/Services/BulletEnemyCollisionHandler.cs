using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Pooling;
using AsteroidGame.Scripts.Signals.Enemies;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Services
{
    public sealed class BulletEnemyCollisionHandler
    {
        private readonly BulletEnemyCollisionContactResolver _contactResolver;
        private readonly BulletPool _bulletPool;
        private readonly SignalBus _signalBus;

        public BulletEnemyCollisionHandler(
            BulletEnemyCollisionContactResolver contactResolver,
            BulletPool bulletPool,
            SignalBus signalBus)
        {
            _contactResolver = contactResolver;
            _bulletPool = bulletPool;
            _signalBus = signalBus;
        }

        public void Handle(CollisionContact contact)
        {
            if (!_contactResolver.TryResolve(contact, out BulletEnemyCollisionContact bulletEnemyContact))
                return;
            
            if (!_bulletPool.ReleaseByCollisionBody(bulletEnemyContact.BulletBody))
                return;
            
            _signalBus.Fire(new EnemyHitByBulletSignal(bulletEnemyContact.EnemyBody));
        }
    }
}