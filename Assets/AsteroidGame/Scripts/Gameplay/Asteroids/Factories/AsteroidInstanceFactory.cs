using AsteroidGame.Scripts.Domain.Asteroids.Models;
using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Mapping;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Contracts;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;
using AsteroidGame.Scripts.Gameplay.Enemies.Factories;
using AsteroidGame.Scripts.Gameplay.Factories;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Factories
{
    public sealed class AsteroidInstanceFactory
    {
        private readonly Body2DFactory _bodyFactory;
        private readonly CollisionBodyFactory _collisionBodyFactory;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly EnemyCollisionCategoryMapper _categoryMapper;
        private readonly AsteroidSettings _settings;
        private readonly EnemyModelFactory _enemyModelFactory;
        private readonly AsteroidModelFactory _asteroidModelFactory;
        private readonly AsteroidInstanceZenjectFactory _asteroidInstanceFactory;
        private readonly IAsteroidViewFactory _viewFactory;

        public AsteroidInstanceFactory(
            Body2DFactory bodyFactory,
            CollisionBodyFactory collisionBodyFactory,
            PhysicsValueFactory physicsValueFactory,
            EnemyCollisionCategoryMapper categoryMapper,
            AsteroidSettings settings,
            EnemyModelFactory enemyModelFactory,
            AsteroidModelFactory asteroidModelFactory,
            AsteroidInstanceZenjectFactory asteroidInstanceFactory,
            IAsteroidViewFactory viewFactory)
        {
            _bodyFactory = bodyFactory;
            _collisionBodyFactory = collisionBodyFactory;
            _physicsValueFactory = physicsValueFactory;
            _categoryMapper = categoryMapper;
            _settings = settings;
            _enemyModelFactory = enemyModelFactory;
            _asteroidModelFactory = asteroidModelFactory;
            _asteroidInstanceFactory = asteroidInstanceFactory;
            _viewFactory = viewFactory;
        }

        public AsteroidInstance Create(EnemyType type)
        {
            Vector2D position = _physicsValueFactory.CreateVector(0f, 0f);
            Velocity velocity = _physicsValueFactory.CreateVelocity(position);
            Body2D body = _bodyFactory.Create(position, velocity, 0f);
            EnemyModel enemy = _enemyModelFactory.Create(type, body);
            AsteroidModel asteroid = _asteroidModelFactory.Create(enemy);
            CollisionBody collisionBody = _collisionBodyFactory.Create(
                _categoryMapper.ToCollisionCategory(type),
                body,
                _settings.GetRadius(type));

            IAsteroidView view = _viewFactory.Create(type);

            return _asteroidInstanceFactory.Create(asteroid, collisionBody, view);
        }
    }
}