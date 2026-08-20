using System;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Gameplay.Factories;

namespace AsteroidGame.Scripts.Gameplay.Player.Factories
{
    public sealed class PlayerModelCreationFactory
    {
        private readonly PlayerMovementSettings _movementSettings;
        private readonly Body2DFactory _bodyFactory;
        private readonly PlayerModelFactory _playerModelFactory;

        public PlayerModelCreationFactory(
            PlayerMovementSettings movementSettings,
            Body2DFactory bodyFactory,
            PlayerModelFactory playerModelFactory)
        {
            _movementSettings = movementSettings ?? throw new ArgumentNullException(nameof(movementSettings));
            _bodyFactory = bodyFactory ?? throw new ArgumentNullException(nameof(bodyFactory));
            _playerModelFactory = playerModelFactory ?? throw new ArgumentNullException(nameof(playerModelFactory));
        }

        public PlayerModel Create()
        {
            Body2D body = _bodyFactory.Create(
                _movementSettings.SpawnPosition,
                _movementSettings.InitialVelocity,
                _movementSettings.SpawnRotationDegrees);
            
            return _playerModelFactory.Create(body);
        }
    }
}