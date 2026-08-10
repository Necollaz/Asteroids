using AsteroidGame.Scripts.Domain.Physics;
using AsteroidGame.Scripts.Domain.Player;

namespace AsteroidGame.Scripts.Gameplay.Player
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