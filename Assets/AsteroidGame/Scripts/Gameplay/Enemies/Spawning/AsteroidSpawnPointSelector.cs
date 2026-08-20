using AsteroidGame.Scripts.Domain.Enemies.Settings;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Random;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Gameplay.Asteroids.Spawning;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Spawning
{
    public sealed class AsteroidSpawnPointSelector
    {
        private readonly WorldBounds _worldBounds;
        private readonly EnemySpawnSettings _settings;
        private readonly OutsideWorldSpawnPointSelector _outsideWorldSpawnPointSelector;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly IRandomValueProvider _random;

        public AsteroidSpawnPointSelector(
            WorldBounds worldBounds,
            EnemySpawnSettings settings,
            OutsideWorldSpawnPointSelector outsideWorldSpawnPointSelector,
            PhysicsValueFactory physicsValueFactory,
            IRandomValueProvider random)
        {
            _worldBounds = worldBounds;
            _settings = settings;
            _outsideWorldSpawnPointSelector = outsideWorldSpawnPointSelector;
            _physicsValueFactory = physicsValueFactory;
            _random = random;
        }

        public AsteroidSpawnData Select()
        {
            Vector2D position = _outsideWorldSpawnPointSelector.Select(_settings.AsteroidSpawnMargin);
            Vector2D target = SelectTargetInsideWorld();
            Vector2D direction = target.Subtract(position).Normalized;

            return new AsteroidSpawnData(position, direction);
        }

        private Vector2D SelectTargetInsideWorld() => _physicsValueFactory.CreateVector(
            _random.Range(-_worldBounds.HalfWidth, _worldBounds.HalfWidth),
            _random.Range(-_worldBounds.HalfHeight, _worldBounds.HalfHeight));
    }
}