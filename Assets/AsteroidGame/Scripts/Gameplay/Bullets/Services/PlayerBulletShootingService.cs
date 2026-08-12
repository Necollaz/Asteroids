using Zenject;
using AsteroidGame.Scripts.Domain.Bullets.Settings;
using AsteroidGame.Scripts.Domain.Physics.Calculations;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Pooling;
using AsteroidGame.Scripts.Gameplay.Bullets.Timers;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Input;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Services
{
    public sealed class PlayerBulletShootingService : ITickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly IPlayerBodyProvider _playerBodyProvider;
        private readonly IPlayerControlState _playerControlState;
        private readonly IGameplayPauseState _gameplayPauseState;
        private readonly ITimeProvider _timeProvider;
        private readonly Direction2DCalculator _direction2DCalculator;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly BulletSettings _bulletSettings;
        private readonly BulletPool _bulletPool;
        private readonly BulletFireCooldown _bulletFireCooldown;

        public PlayerBulletShootingService(
            IPlayerInput playerInput,
            IPlayerBodyProvider playerBodyProvider,
            IPlayerControlState playerControlState,
            IGameplayPauseState gameplayPauseState,
            ITimeProvider timeProvider,
            Direction2DCalculator direction2DCalculator,
            PhysicsValueFactory physicsValueFactory,
            BulletSettings bulletSettings,
            BulletPool bulletPool,
            BulletFireCooldown bulletFireCooldown)
        {
            _playerInput = playerInput;
            _playerBodyProvider = playerBodyProvider;
            _playerControlState = playerControlState;
            _gameplayPauseState = gameplayPauseState;
            _timeProvider = timeProvider;
            _direction2DCalculator = direction2DCalculator;
            _physicsValueFactory = physicsValueFactory;
            _bulletSettings = bulletSettings;
            _bulletPool = bulletPool;
            _bulletFireCooldown = bulletFireCooldown;
        }

        void ITickable.Tick()
        {
            if (_gameplayPauseState.IsPaused)
                return;

            float deltaTime = _timeProvider.DeltaTime;
            _bulletFireCooldown.Tick(deltaTime);

            if (!_playerControlState.CanControl)
                return;

            PlayerInputState inputState = _playerInput.GetState();

            if (!inputState.IsBulletFirePressed || !_bulletFireCooldown.IsReady)
                return;

            Shoot();
        }

        private void Shoot()
        {
            Body2D playerBody = _playerBodyProvider.Body;
            Vector2D forward = _direction2DCalculator.FromAngleDegrees(playerBody.RotationDegrees);
            Vector2D spawnPosition = playerBody.Position.Add(forward.Multiply(_bulletSettings.SpawnOffset));
            Velocity velocity = _physicsValueFactory.CreateVelocity(forward.Multiply(_bulletSettings.Speed));

            if (!_bulletPool.TrySpawn(spawnPosition, velocity, playerBody.RotationDegrees))
                return;

            _bulletFireCooldown.Restart();
        }
    }
}