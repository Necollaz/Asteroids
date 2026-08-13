using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Gameplay.Asteroids.Spawning;
using AsteroidGame.Scripts.Gameplay.Random;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Spawning
{
    public sealed class AsteroidSpawnPointSelector
    {
        private readonly WorldBounds _worldBounds;
        private readonly AsteroidSettings _settings;
        private readonly OutsideWorldSpawnPointSelector _outsideWorldSpawnPointSelector;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly IRandomValueProvider _random;

        public AsteroidSpawnPointSelector(
            WorldBounds worldBounds,
            AsteroidSettings settings,
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
            Vector2D position = _outsideWorldSpawnPointSelector.Select(_settings.SpawnMargin);
            Vector2D target = SelectTargetInsideWorld();
            Vector2D direction = target.Subtract(position).Normalized;
            
            return new AsteroidSpawnData(position, direction);
        }
        
        private Vector2D SelectTargetInsideWorld() => _physicsValueFactory.CreateVector(
            _random.Range(-_worldBounds.HalfWidth, _worldBounds.HalfWidth),
            _random.Range(-_worldBounds.HalfHeight, _worldBounds.HalfHeight));
    }
}