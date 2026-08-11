using System;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.World.Settings;

namespace AsteroidGame.Scripts.Domain.World.Bounds
{
    public sealed class WorldBounds
    {
        private readonly PhysicsValueFactory _physicsValueFactory;

        public WorldBounds(WorldSettings worldSettings, PhysicsValueFactory physicsValueFactory)
        {
            Width = worldSettings.Width;
            Height = worldSettings.Height;
            _physicsValueFactory = physicsValueFactory;
        }

        public float Width { get; }
        public float Height { get; }
        public float HalfWidth => Width * 0.5f;
        public float HalfHeight => Height * 0.5f;

        public Vector2D WrapPosition(Vector2D position)
        {
            float x = position.X;
            float y = position.Y;

            while (x > HalfWidth)
                x -= Width;

            while (x < -HalfWidth)
                x += Width;

            while (y > HalfHeight)
                y -= Height;

            while (y < -HalfHeight)
                y += Height;

            return _physicsValueFactory.CreateVector(x, y);
        }
        
        public bool Contains(Vector2D position, float margin)
        {
            if (margin < 0f)
                throw new ArgumentOutOfRangeException(nameof(margin));

            return position.X >= -HalfWidth - margin &&
                   position.X <= HalfWidth + margin &&
                   position.Y >= -HalfHeight - margin &&
                   position.Y <= HalfHeight + margin;
        }
    }
}