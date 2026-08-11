using System;
using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Bullets.Settings;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Contracts;
using AsteroidGame.Scripts.Gameplay.Bullets.Factories;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Pooling
{
    public sealed class BulletPool : IInitializable
    {
        private readonly BulletSettings _settings;
        private readonly IBulletViewFactory _viewFactory;
        private readonly BulletInstanceFactory _instanceFactory;
        private readonly CollisionBodyRegistry _collisionBodyRegistry;
        private readonly Queue<BulletInstance> _availableBullets = new();
        private readonly List<BulletInstance> _activeBullets = new();

        private int _createdCount;

        public BulletPool(
            BulletSettings settings,
            IBulletViewFactory viewFactory,
            BulletInstanceFactory instanceFactory,
            CollisionBodyRegistry collisionBodyRegistry)
        {
            _settings = settings;
            _viewFactory = viewFactory;
            _instanceFactory = instanceFactory;
            _collisionBodyRegistry = collisionBodyRegistry;
        }

        public IReadOnlyList<BulletInstance> ActiveBullets => _activeBullets;

        void IInitializable.Initialize() => WarmUp();

        public bool TrySpawn(Vector2D position, Velocity velocity, float rotationDegrees)
        {
            if (!TryGet(out BulletInstance bullet))
                return false;

            bullet.Activate(position, velocity, rotationDegrees);

            return true;
        }

        public void Release(BulletInstance bullet)
        {
            if (bullet == null)
                throw new ArgumentNullException(nameof(bullet));

            if (!_activeBullets.Remove(bullet))
                throw new InvalidOperationException("Bullet is already released or does not belong to active pool.");

            bullet.Deactivate();
            _availableBullets.Enqueue(bullet);
        }

        public bool ReleaseByCollisionBody(CollisionBody collisionBody)
        {
            if (collisionBody == null)
                throw new ArgumentNullException(nameof(collisionBody));

            for (int i = _activeBullets.Count - 1; i >= 0; i--)
            {
                BulletInstance bullet = _activeBullets[i];

                if (!ReferenceEquals(bullet.CollisionBody, collisionBody))
                    continue;

                Release(bullet);

                return true;
            }

            return false;
        }

        public void ReleaseAll()
        {
            for (int i = _activeBullets.Count - 1; i >= 0; i--)
                Release(_activeBullets[i]);
        }

        private void WarmUp()
        {
            for (int i = 0; i < _settings.PoolSize; i++)
                _availableBullets.Enqueue(CreateBullet());
        }

        private bool TryGet(out BulletInstance bullet)
        {
            bullet = null;

            if (_availableBullets.Count == 0 && _createdCount >= _settings.PoolSize)
                return false;

            bullet = _availableBullets.Count > 0 ? _availableBullets.Dequeue() : CreateBullet();
            _activeBullets.Add(bullet);

            return true;
        }

        private BulletInstance CreateBullet()
        {
            IBulletView view = _viewFactory.Create();
            BulletInstance bullet = _instanceFactory.Create(view, _settings);
            bullet.Deactivate();
            _collisionBodyRegistry.Register(bullet.CollisionBody);
            
            _createdCount++;

            return bullet;
        }
    }
}