using AsteroidGame.Scripts.Domain.Asteroids.Models;
using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Enemies.Mapping;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Gameplay.Asteroids.Contracts;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;
using AsteroidGame.Scripts.Gameplay.Enemies.Factories;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Factories
{
    public sealed class AsteroidInstanceFactory
    {
        private readonly EnemyCollisionCategoryMapper _categoryMapper;
        private readonly AsteroidSettings _settings;
        private readonly EnemyInstanceContextFactory _enemyContextFactory;
        private readonly AsteroidModelFactory _asteroidModelFactory;
        private readonly AsteroidInstanceZenjectFactory _asteroidInstanceFactory;
        private readonly IAsteroidViewFactory _viewFactory;

        public AsteroidInstanceFactory(
            EnemyCollisionCategoryMapper categoryMapper,
            AsteroidSettings settings,
            EnemyInstanceContextFactory enemyContextFactory,
            AsteroidModelFactory asteroidModelFactory,
            AsteroidInstanceZenjectFactory asteroidInstanceFactory,
            IAsteroidViewFactory viewFactory)
        {
            _categoryMapper = categoryMapper;
            _settings = settings;
            _enemyContextFactory = enemyContextFactory;
            _asteroidModelFactory = asteroidModelFactory;
            _asteroidInstanceFactory = asteroidInstanceFactory;
            _viewFactory = viewFactory;
        }


        public AsteroidInstance Create(EnemyType type)
        {
            EnemyInstanceContext context = _enemyContextFactory.Create(
                type,
                _categoryMapper.ToCollisionCategory(type),
                _settings.GetRadius(type));
            AsteroidModel asteroid = _asteroidModelFactory.Create(context.Enemy);
            IAsteroidView view = _viewFactory.Create(type);

            return _asteroidInstanceFactory.Create(asteroid, context.CollisionBody, view);
        }
    }
}