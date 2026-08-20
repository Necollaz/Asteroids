using System;
using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Settings;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Factories;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Pooling
{
    public sealed class AsteroidPool : IInitializable
    {
        private readonly EnemySpawnSettings _settings;
        private readonly AsteroidInstanceFactory _instanceFactory;
        private readonly CollisionBodyRegistry _collisionBodyRegistry;
        private readonly Dictionary<EnemyType, Queue<AsteroidInstance>> _availableAsteroids = new();
        private readonly List<AsteroidInstance> _activeAsteroids = new();

        private int _createdCount;

        public AsteroidPool(
            EnemySpawnSettings settings,
            AsteroidInstanceFactory instanceFactory,
            CollisionBodyRegistry collisionBodyRegistry)
        {
            _settings = settings;
            _instanceFactory = instanceFactory;
            _collisionBodyRegistry = collisionBodyRegistry;
        }
        
        public IReadOnlyList<AsteroidInstance> ActiveAsteroids => _activeAsteroids;

        void IInitializable.Initialize()
        {
            CreateQueue(EnemyType.LargeAsteroid);
            CreateQueue(EnemyType.MediumAsteroid);
            CreateQueue(EnemyType.SmallAsteroid);
            WarmUp();
        }

        public bool TrySpawn(EnemyType type, Vector2D position, Velocity velocity, float rotationDegrees)
        {
            if (!TryGet(type, out AsteroidInstance asteroid))
                return false;

            asteroid.Activate(position, velocity, rotationDegrees);

            return true;
        }

        public bool ReleaseByCollisionBody(CollisionBody collisionBody, out AsteroidInstance asteroid)
        {
            asteroid = null;

            if (collisionBody == null)
                throw new ArgumentNullException(nameof(collisionBody));

            for (int i = _activeAsteroids.Count - 1; i >= 0; i--)
            {
                AsteroidInstance current = _activeAsteroids[i];

                if (!ReferenceEquals(current.CollisionBody, collisionBody))
                    continue;

                asteroid = current;
                Release(current);

                return true;
            }

            return false;
        }

        public void Release(AsteroidInstance asteroid)
        {
            if (asteroid == null)
                throw new ArgumentNullException(nameof(asteroid));

            if (!_activeAsteroids.Remove(asteroid))
                throw new InvalidOperationException("Asteroid is already released or does not belong to active pool.");

            asteroid.Deactivate();
            _availableAsteroids[asteroid.Type].Enqueue(asteroid);
        }
        
        private AsteroidInstance CreateAsteroid(EnemyType type)
        {
            AsteroidInstance asteroid = _instanceFactory.Create(type);
            asteroid.Deactivate();
            _collisionBodyRegistry.Register(asteroid.CollisionBody);
            _createdCount++;

            return asteroid;
        }
        
        private bool TryGet(EnemyType type, out AsteroidInstance asteroid)
        {
            asteroid = null;
            Queue<AsteroidInstance> queue = _availableAsteroids[type];

            if (queue.Count == 0 && _createdCount >= _settings.AsteroidPoolSize)
                return false;

            asteroid = queue.Count > 0 ? queue.Dequeue() : CreateAsteroid(type);
            _activeAsteroids.Add(asteroid);

            return true;
        }

        private void CreateQueue(EnemyType type) => _availableAsteroids.Add(type, new Queue<AsteroidInstance>());
        
        private void WarmUpType(EnemyType type, int count)
        {
            for (int i = 0; i < count; i++)
                _availableAsteroids[type].Enqueue(CreateAsteroid(type));
        }
        
        private void WarmUp()
        {
            int perTypeCount = Math.Max(1, _settings.AsteroidPoolSize / 3);

            WarmUpType(EnemyType.LargeAsteroid, perTypeCount);
            WarmUpType(EnemyType.MediumAsteroid, perTypeCount);
            WarmUpType(EnemyType.SmallAsteroid, perTypeCount);
        }
    }
}