using System;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Physics.Calculations
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