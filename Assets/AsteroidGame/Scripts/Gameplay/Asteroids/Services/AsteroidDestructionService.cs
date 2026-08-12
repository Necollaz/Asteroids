using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Pooling;
using AsteroidGame.Scripts.Gameplay.Random;
using AsteroidGame.Scripts.Signals.Enemies;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Services
{
    public sealed class AsteroidDestructionService : IInitializable, IDisposable
    {
        private readonly AsteroidPool _pool;
        private readonly AsteroidSettings _settings;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly IRandomValueProvider _random;
        private readonly SignalBus _signalBus;

        public AsteroidDestructionService(
            AsteroidPool pool,
            AsteroidSettings settings,
            PhysicsValueFactory physicsValueFactory,
            IRandomValueProvider random,
            SignalBus signalBus)
        {
            _pool = pool;
            _settings = settings;
            _physicsValueFactory = physicsValueFactory;
            _random = random;
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

        private Vector2D CreateRandomDirection()
        {
            float x = _random.Range(-1f, 1f);
            float y = _random.Range(-1f, 1f);
            Vector2D direction = _physicsValueFactory.CreateVector(x, y);

            if (direction.SqrMagnitude <= float.Epsilon)
                return _physicsValueFactory.CreateVector(0f, 1f);

            return direction.Normalized;
        }
        
        private void HandleBulletHit(EnemyHitByBulletSignal signal) => DestroyAsteroid(signal.EnemyBody);

        private void HandleLaserHit(EnemyHitByLaserSignal signal) => DestroyAsteroid(signal.EnemyBody);

        private void DestroyAsteroid(CollisionBody enemyBody)
        {
            if (!_pool.ReleaseByCollisionBody(enemyBody, out AsteroidInstance asteroid))
                return;

            EnemyType destroyedType = asteroid.Type;
            Vector2D position = asteroid.Body.Position;

            _signalBus.Fire(new EnemyDestroyedSignal(destroyedType, position));

            if (!_settings.TryGetFragmentType(destroyedType, out EnemyType fragmentType, out int count))
                return;

            SpawnFragments(fragmentType, count, position);
        }

        private void SpawnFragments(EnemyType fragmentType, int count, Vector2D position)
        {
            for (int i = 0; i < count; i++)
            {
                Vector2D direction = CreateRandomDirection();
                float speed = _settings.GetSpeed(fragmentType);
                Velocity velocity = _physicsValueFactory.CreateVelocity(direction.Multiply(speed));

                _pool.TrySpawn(fragmentType, position, velocity, 0f);
            }
        }

    }
}