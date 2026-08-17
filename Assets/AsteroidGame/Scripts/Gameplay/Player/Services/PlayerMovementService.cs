using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Physics.Services;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Player.Calculations;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Input;

namespace AsteroidGame.Scripts.Gameplay.Player.Services
{
    public sealed class PlayerMovementService : IFixedTickable
    {
        private readonly IPlayerBodyProvider _playerBodyProvider;
        private readonly IPlayerInput _playerInput;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;
        private readonly IPlayerControlState _controlState;
        private readonly CustomPhysicsWorld _physicsWorld;
        private readonly PlayerMovementSettings _settings;
        private readonly PlayerAccelerationCalculator _accelerationCalculator;

        public PlayerMovementService(
            IPlayerBodyProvider playerBodyProvider,
            IPlayerInput playerInput,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState,
            IPlayerControlState controlState,
            CustomPhysicsWorld physicsWorld,
            PlayerMovementSettings settings,
            PlayerAccelerationCalculator accelerationCalculator)
        {
            _playerBodyProvider = playerBodyProvider;
            _playerInput = playerInput;
            _timeProvider = timeProvider;
            _pauseState = pauseState;
            _controlState = controlState;
            _physicsWorld = physicsWorld;
            _settings = settings;
            _accelerationCalculator = accelerationCalculator;
        }

        void IFixedTickable.FixedTick()
        {
            if (_pauseState.IsPaused)
                return;

            float deltaTime = _timeProvider.FixedDeltaTime;

            if (!_controlState.CanControl)
            {
                MoveWithoutControl(deltaTime);
                
                return;
            }

            PlayerInputState inputState = _playerInput.GetState();
            Rotate(inputState, deltaTime);
            Move(inputState, deltaTime);
        }

        private void Rotate(PlayerInputState inputState, float deltaTime)
        {
            Body2D body = _playerBodyProvider.Body;
            float rotationDelta = inputState.TurnDirection * _settings.TurnSpeedDegreesPerSecond * deltaTime;
            body.SetRotation(body.RotationDegrees + rotationDelta);
        }

        private void Move(PlayerInputState inputState, float deltaTime)
        {
            Body2D body = _playerBodyProvider.Body;
            Acceleration acceleration = _accelerationCalculator.Calculate(inputState, _settings, body.RotationDegrees);
            StepBody(body, acceleration, deltaTime);
        }

        private void MoveWithoutControl(float deltaTime) => 
            StepBody(_playerBodyProvider.Body, _settings.NoAcceleration, deltaTime);

        private void StepBody(Body2D body, Acceleration acceleration, float deltaTime) =>
            _physicsWorld.Step(
            body,
            acceleration,
            deltaTime,
            _settings.MaxSpeed,
            _settings.LinearDamping);
    }
}