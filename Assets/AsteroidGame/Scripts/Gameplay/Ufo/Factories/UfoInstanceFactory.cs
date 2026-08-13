using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Ufo.Models;
using AsteroidGame.Scripts.Domain.Ufo.Settings;
using AsteroidGame.Scripts.Gameplay.Enemies.Factories;
using AsteroidGame.Scripts.Gameplay.Factories;
using AsteroidGame.Scripts.Gameplay.Ufo.Contracts;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.States;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Factories
{
    public sealed class UfoInstanceFactory
    {
        private readonly Body2DFactory _bodyFactory;
        private readonly CollisionBodyFactory _collisionBodyFactory;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly EnemyModelFactory _enemyModelFactory;
        private readonly UfoModelFactory _ufoModelFactory;
        private readonly UfoInstanceZenjectFactory _ufoInstanceFactory;
        private readonly UfoKnockbackStateFactory _knockbackStateFactory;
        private readonly IUfoViewFactory _viewFactory;
        private readonly UfoSettings _settings;

        public UfoInstanceFactory(
            Body2DFactory bodyFactory,
            CollisionBodyFactory collisionBodyFactory,
            PhysicsValueFactory physicsValueFactory,
            EnemyModelFactory enemyModelFactory,
            UfoModelFactory ufoModelFactory,
            UfoInstanceZenjectFactory ufoInstanceFactory,
            UfoKnockbackStateFactory knockbackStateFactory,
            IUfoViewFactory viewFactory,
            UfoSettings settings)
        {
            _bodyFactory = bodyFactory;
            _collisionBodyFactory = collisionBodyFactory;
            _physicsValueFactory = physicsValueFactory;
            _enemyModelFactory = enemyModelFactory;
            _ufoModelFactory = ufoModelFactory;
            _ufoInstanceFactory = ufoInstanceFactory;
            _knockbackStateFactory = knockbackStateFactory;
            _viewFactory = viewFactory;
            _settings = settings;
        }

        public UfoInstance Create()
        {
            Vector2D position = _physicsValueFactory.CreateVector(0f, 0f);
            Velocity velocity = _physicsValueFactory.CreateVelocity(position);
            Body2D body = _bodyFactory.Create(position, velocity, 0f);
            EnemyModel enemy = _enemyModelFactory.Create(EnemyType.Ufo, body);
            UfoModel ufo = _ufoModelFactory.Create(enemy);
            CollisionBody collisionBody = _collisionBodyFactory.Create(
                CollisionCategory.Ufo,
                body,
                _settings.CollisionRadius);
            IUfoView view = _viewFactory.Create();
            UfoKnockbackState knockbackState = _knockbackStateFactory.Create();

            return _ufoInstanceFactory.Create(ufo, collisionBody, view, knockbackState);
        }
    }
}