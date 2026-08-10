using Zenject;
using AsteroidGame.Scripts.Domain.Physics;
using AsteroidGame.Scripts.Domain.Player;
using AsteroidGame.Scripts.Gameplay.Factories;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Input;

namespace AsteroidGame.Scripts.Gameplay.Player.Services
{
    public sealed class PlayerMovementService : ITickable, IPlayerStateProvider, IPlayerBodyProvider
    {
        private readonly PlayerMovementSettings _settings;
        private readonly PlayerAccelerationCalculator _accelerationCalculator;
        private readonly CustomPhysicsWorld _physicsWorld;
        private readonly Body2D _body;
        private readonly IPlayerInput _playerInput;
        private readonly ITimeProvider _timeProvider;
        private readonly IPlayerControlState _playerControlState;

        public PlayerMovementService(
            IPlayerInput playerInput,
            ITimeProvider timeProvider,
            IPlayerControlState playerControlState,
            CustomPhysicsWorld physicsWorld,
            PlayerMovementSettings settings,
            PlayerAccelerationCalculator accelerationCalculator,
            Body2DFactory bodyFactory)
        {
            _playerInput = playerInput;
            _timeProvider = timeProvider;
            _physicsWorld = physicsWorld;
            _settings = settings;
            _accelerationCalculator = accelerationCalculator;
            _playerControlState = playerControlState;
            _body = bodyFactory.Create(
                _settings.SpawnPosition,
                _settings.InitialVelocity,
                _settings.SpawnRotationDegrees);
        }
        
        public Body2D Body => _body;
        public PlayerSnapshot Snapshot => new(_body.Position, _body.Velocity.Value, _body.RotationDegrees);

        void ITickable.Tick()
        {
            float deltaTime = _timeProvider.DeltaTime;

            if (!_playerControlState.CanControl)
            {
                _physicsWorld.Step(
                    _body,
                    _settings.NoAcceleration,
                    deltaTime,
                    _settings.MaxSpeed,
                    _settings.LinearDamping);
                
                return;
            }

            PlayerInputState inputState = _playerInput.GetState();

            Rotate(inputState, deltaTime);
            Move(inputState, deltaTime);
        }

        private void Rotate(PlayerInputState inputState, float deltaTime)
        {
            float rotationDelta = inputState.TurnDirection * _settings.TurnSpeedDegreesPerSecond * deltaTime;
            _body.SetRotation(_body.RotationDegrees + rotationDelta);
        }

        private void Move(PlayerInputState inputState, float deltaTime)
        {
            Acceleration acceleration = _accelerationCalculator.Calculate(inputState, _settings, _body.RotationDegrees);
            _physicsWorld.Step(_body, acceleration, deltaTime, _settings.MaxSpeed, _settings.LinearDamping);
        }
    }
}