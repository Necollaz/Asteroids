using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Pooling;
using AsteroidGame.Scripts.Signals.Enemies;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Services
{
    public sealed class UfoDestructionService : IInitializable, IDisposable
    {
        private readonly UfoPool _pool;
        private readonly SignalBus _signalBus;

        public UfoDestructionService(UfoPool pool, SignalBus signalBus)
        {
            _pool = pool;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<EnemyHitByBulletSignal>(HandleBulletHit);
            _signalBus.Subscribe<EnemyHitByLaserSignal>(HandleLaserHit);
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<EnemyHitByBulletSignal>(HandleBulletHit);
            _signalBus.Unsubscribe<EnemyHitByLaserSignal>(HandleLaserHit);
        }

        private void HandleBulletHit(EnemyHitByBulletSignal signal) => DestroyUfo(signal.EnemyBody);

        private void HandleLaserHit(EnemyHitByLaserSignal signal) => DestroyUfo(signal.EnemyBody);

        private void DestroyUfo(CollisionBody enemyBody)
        {
            if (!_pool.ReleaseByCollisionBody(enemyBody, out UfoInstance ufo))
                return;

            Vector2D position = ufo.Body.Position;
            _signalBus.Fire(new EnemyDestroyedSignal(EnemyType.Ufo, position));
        }
    }
}