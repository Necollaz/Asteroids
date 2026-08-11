using AsteroidGame.Scripts.Domain.Physics.Calculations;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;

namespace AsteroidGame.Scripts.Gameplay.Player.Calculations
{
    public sealed class PlayerAccelerationCalculator
    {
        private readonly Direction2DCalculator _direction2DCalculator;
        private readonly PhysicsValueFactory _physicsValueFactory;

        public PlayerAccelerationCalculator(
            Direction2DCalculator direction2DCalculator,
            PhysicsValueFactory physicsValueFactory)
        {
            _direction2DCalculator = direction2DCalculator;
            _physicsValueFactory = physicsValueFactory;
        }

        public Acceleration Calculate(
            PlayerInputState inputState,
            PlayerMovementSettings settings,
            float rotationDegrees)
        {
            if (!inputState.IsThrustPressed)
                return settings.NoAcceleration;
            
            Vector2D forward = _direction2DCalculator.FromAngleDegrees(rotationDegrees);
            Vector2D acceleration = forward.Multiply(settings.Acceleration);
            
            return _physicsValueFactory.CreateAcceleration(acceleration);
        }
    }
}