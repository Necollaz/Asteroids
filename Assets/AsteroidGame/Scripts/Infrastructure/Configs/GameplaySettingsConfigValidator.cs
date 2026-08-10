using System;
using UnityEngine;
using Zenject;

namespace AsteroidGame.Scripts.Infrastructure.Configs
{
    public sealed class GameplaySettingsConfigValidator : IInitializable
    {
        private readonly GameplaySettingsConfig _config;
        
        public GameplaySettingsConfigValidator(GameplaySettingsConfig config) => _config = config;

        void IInitializable.Initialize()
        {
            if (_config.PlayerAcceleration <= 0f)
                throw new InvalidOperationException("Player acceleration must be greater than zero.");

            if (_config.PlayerTurnSpeed <= 0f)
                throw new InvalidOperationException("Player turn speed must be greater than zero.");

            if (_config.PlayerMaxSpeed <= 0f)
                throw new InvalidOperationException("Player max speed must be greater than zero.");

            if (_config.PlayerLinearDamping < 0f)
                throw new InvalidOperationException("Player linear damping cannot be negative.");

            ValidateKey(_config.TurnLeftKey, nameof(_config.TurnLeftKey));
            ValidateKey(_config.TurnRightKey, nameof(_config.TurnRightKey));
            ValidateKey(_config.ThrustKey, nameof(_config.ThrustKey));
            ValidateKey(_config.AlternativeTurnLeftKey, nameof(_config.AlternativeTurnLeftKey));
            ValidateKey(_config.AlternativeTurnRightKey, nameof(_config.AlternativeTurnRightKey));
            ValidateKey(_config.AlternativeThrustKey, nameof(_config.AlternativeThrustKey));
        }

        private void ValidateKey(KeyCode keyCode, string propertyName)
        {
            if (keyCode == KeyCode.None)
                throw new InvalidOperationException($"{propertyName} must be assigned.");
        }
    }
}