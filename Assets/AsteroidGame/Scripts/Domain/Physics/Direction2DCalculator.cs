using System;

namespace AsteroidGame.Scripts.Domain.Physics
{
    public sealed class Direction2DCalculator
    {
        private readonly PhysicsValueFactory _physicsValueFactory;
        
        public Direction2DCalculator(PhysicsValueFactory physicsValueFactory) =>
            _physicsValueFactory = physicsValueFactory;

        public Vector2D FromAngleDegrees(float degrees)
        {
            double radians = degrees * Math.PI / 180d;
            
            return _physicsValueFactory.CreateVector(-(float)Math.Sin(radians), (float)Math.Cos(radians));
        }
    }
}