using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Gameplay.Random;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Spawning
{
    public sealed class AsteroidSpawnPointSelector
    {
        private const int LeftSide = 0;
        private const int RightSide = 1;
        private const int TopSide = 2;
        private const int BottomSide = 3;
        private const int SideCount = 4;
        
        private readonly WorldBounds _worldBounds;
        private readonly AsteroidSettings _settings;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly IRandomValueProvider _random;

        public AsteroidSpawnPointSelector(
            WorldBounds worldBounds,
            AsteroidSettings settings,
            PhysicsValueFactory physicsValueFactory,
            IRandomValueProvider random)
        {
            _worldBounds = worldBounds;
            _settings = settings;
            _physicsValueFactory = physicsValueFactory;
            _random = random;
        }

        public AsteroidSpawnData Select()
        {
            Vector2D position = SelectPosition();
            Vector2D target = SelectTargetInsideWorld();
            Vector2D direction = target.Subtract(position).Normalized;
            
            return new AsteroidSpawnData(position, direction);
        }

        private Vector2D SelectPosition()
        {
            int side = _random.Range(0, SideCount);
            float x = _random.Range(-_worldBounds.HalfWidth, _worldBounds.HalfWidth);
            float y = _random.Range(_worldBounds.HalfHeight, _worldBounds.HalfHeight);
            float margin = _settings.SpawnMargin;

            return side switch
            {
                LeftSide => _physicsValueFactory.CreateVector(-_worldBounds.HalfWidth - margin, y),
                RightSide => _physicsValueFactory.CreateVector(_worldBounds.HalfWidth + margin, y),
                TopSide => _physicsValueFactory.CreateVector(x, _worldBounds.HalfHeight + margin),
                BottomSide => _physicsValueFactory.CreateVector(x, -_worldBounds.HalfHeight - margin),
                _ => _physicsValueFactory.CreateVector(x, y)
            };
        }
        
        private Vector2D SelectTargetInsideWorld() => _physicsValueFactory.CreateVector(
            _random.Range(-_worldBounds.HalfWidth, _worldBounds.HalfWidth),
            _random.Range(-_worldBounds.HalfHeight, _worldBounds.HalfHeight));
    }
}