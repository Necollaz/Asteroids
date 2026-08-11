using System;
using AsteroidGame.Scripts.Domain.Bullets.Models;
using AsteroidGame.Scripts.Domain.Bullets.Settings;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Contracts;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;
using AsteroidGame.Scripts.Gameplay.Factories;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Factories
{
    public sealed class BulletInstanceFactory
    {
        private readonly BulletModelFactory _bulletModelFactory;
        private readonly Body2DFactory _bodyFactory;
        private readonly CollisionBodyFactory _collisionBodyFactory;
        private readonly PhysicsValueFactory _physicsValueFactory;

        public BulletInstanceFactory(
            BulletModelFactory bulletModelFactory,
            Body2DFactory bodyFactory,
            CollisionBodyFactory collisionBodyFactory,
            PhysicsValueFactory physicsValueFactory)
        {
            _bulletModelFactory = bulletModelFactory;
            _bodyFactory = bodyFactory;
            _collisionBodyFactory = collisionBodyFactory;
            _physicsValueFactory = physicsValueFactory;
        }

        public BulletInstance Create(IBulletView view, BulletSettings settings)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            Vector2D position = _physicsValueFactory.CreateVector(0f, 0f);
            Velocity velocity = _physicsValueFactory.CreateVelocity(position);
            Body2D body = _bodyFactory.Create(position, velocity, 0f);
            CollisionBody collisionBody = _collisionBodyFactory.Create(CollisionCategory.Bullet, body, settings.Radius);
            BulletModel model = _bulletModelFactory.Create(body);

            return new BulletInstance(model, collisionBody, view);
        }
    }
}