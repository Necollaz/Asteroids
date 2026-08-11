using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Physics.Services;
using AsteroidGame.Scripts.Domain.Player.Factories;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Player.Calculations;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Input;

namespace AsteroidGame.Scripts.Gameplay.Player.Services
{
    public sealed class PlayerMovementService : ITickable, IPlayerStateProvider, IPlayerBodyProvider
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerMovementSettings _settings;
        private readonly PlayerAccelerationCalculator _accelerationCalculator;
        private readonly PlayerSnapshotFactory _snapshotFactory;
        private readonly CustomPhysicsWorld _physicsWorld;
        private readonly IPlayerInput _playerInput;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;
        private readonly IPlayerControlState _controlState;

        public PlayerMovementService(
            PlayerModel playerModel,
            IPlayerInput playerInput,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState,
            IPlayerControlState controlState,
            CustomPhysicsWorld physicsWorld,
            PlayerMovementSettings settings,
            PlayerAccelerationCalculator accelerationCalculator,
            PlayerSnapshotFactory snapshotFactory)
        {
            _playerModel = playerModel;
            _playerInput = playerInput;
            _timeProvider = timeProvider;
            _pauseState = pauseState;
            _controlState = controlState;
            _physicsWorld = physicsWorld;
            _settings = settings;
            _accelerationCalculator = accelerationCalculator;
            _snapshotFactory = snapshotFactory;
        }
        
        public Body2D Body => _playerModel.Body;
        public PlayerSnapshot Snapshot => _snapshotFactory.Create(_playerModel);

        void ITickable.Tick()
        {
            if (_pauseState.IsPaused)
                return;

            float deltaTime = _timeProvider.DeltaTime;

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
            float rotationDelta = inputState.TurnDirection * _settings.TurnSpeedDegreesPerSecond * deltaTime;
            _playerModel.Body.SetRotation(_playerModel.Body.RotationDegrees + rotationDelta);
        }

        private void Move(PlayerInputState inputState, float deltaTime)
        {
            Acceleration acceleration = _accelerationCalculator.Calculate(
                inputState,
                _settings,
                _playerModel.Body.RotationDegrees);

            StepBody(acceleration, deltaTime);
        }

        private void MoveWithoutControl(float deltaTime) => StepBody(_settings.NoAcceleration, deltaTime);

        private void StepBody(Acceleration acceleration, float deltaTime) => _physicsWorld.Step(
            _playerModel.Body,
            acceleration,
            deltaTime,
            _settings.MaxSpeed,
            _settings.LinearDamping);
    }
}