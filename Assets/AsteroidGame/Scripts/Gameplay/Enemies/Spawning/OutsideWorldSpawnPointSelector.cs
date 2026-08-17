using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Random;
using AsteroidGame.Scripts.Domain.World.Bounds;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Spawning
{
    public sealed class OutsideWorldSpawnPointSelector
    {
        private const int FirstSideIndex = 0;
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
            WorldSide side = (WorldSide)_random.Range(FirstSideIndex, SideCount);
            float x = _random.Range(-_worldBounds.HalfWidth, _worldBounds.HalfWidth);
            float y = _random.Range(-_worldBounds.HalfHeight, _worldBounds.HalfHeight);

            return side switch
            {
                WorldSide.Left => _physicsValueFactory.CreateVector(-_worldBounds.HalfWidth - margin, y),
                WorldSide.Right => _physicsValueFactory.CreateVector(_worldBounds.HalfWidth + margin, y),
                WorldSide.Top => _physicsValueFactory.CreateVector(x, _worldBounds.HalfHeight + margin),
                WorldSide.Bottom => _physicsValueFactory.CreateVector(x, -_worldBounds.HalfHeight - margin),
                _ => _physicsValueFactory.CreateVector(x, y)
            };
        }
    }
}