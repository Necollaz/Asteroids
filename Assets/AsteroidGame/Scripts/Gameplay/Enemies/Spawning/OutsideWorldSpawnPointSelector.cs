using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Gameplay.Random;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Spawning
{
    public sealed class OutsideWorldSpawnPointSelector
    {
        private const int LeftSide = 0;
        private const int RightSide = 1;
        private const int TopSide = 2;
        private const int BottomSide = 3;
        private const int SideCount = 4;
        
        private readonly WorldBounds _worldBounds;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly IRandomValueProvider _random;

        public OutsideWorldSpawnPointSelector(
            WorldBounds worldBounds,
            PhysicsValueFactory physicsValueFactory,
            IRandomValueProvider random)
        {
            _worldBounds = worldBounds;
            _physicsValueFactory = physicsValueFactory;
            _random = random;
        }

        public Vector2D Select(float margin)
        {
            int side = _random.Range(0, SideCount);
            float x = _random.Range(-_worldBounds.HalfWidth, _worldBounds.HalfWidth);
            float y = _random.Range(-_worldBounds.HalfHeight, _worldBounds.HalfHeight);

            return side switch
            {
                LeftSide => _physicsValueFactory.CreateVector(-_worldBounds.HalfWidth - margin, y),
                RightSide => _physicsValueFactory.CreateVector(_worldBounds.HalfWidth + margin, y),
                TopSide => _physicsValueFactory.CreateVector(x, _worldBounds.HalfHeight + margin),
                BottomSide => _physicsValueFactory.CreateVector(x, _worldBounds.HalfHeight - margin),
                _ => _physicsValueFactory.CreateVector(x, y)
            };
        }
    }
}