using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Loading;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections
{
    public sealed class JsonPlayerSettingsSection :
        IPlayerMovementSettingsData,
        IPlayerCollisionSettingsData,
        IKeyboardInputSettingsData,
        IPlayerInputRouterSettingsData,
        IMobileInputSettingsData
    {
        private readonly PlayerSettingsJson _settings;
        private readonly PlayerInputSourceType _inputSourceType;
        private readonly KeyCode _turnLeftKey;
        private readonly KeyCode _turnRightKey;
        private readonly KeyCode _thrustKey;
        private readonly KeyCode _alternativeTurnLeftKey;
        private readonly KeyCode _alternativeTurnRightKey;
        private readonly KeyCode _alternativeThrustKey;
        private readonly KeyCode _fireBulletKey;
        private readonly KeyCode _alternativeFireBulletKey;
        private readonly KeyCode _fireLaserKey;
        private readonly KeyCode _alternativeFireLaserKey;

        public JsonPlayerSettingsSection(PlayerSettingsJson settings, KeyCodeParser keyCodeParser)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            if (keyCodeParser == null)
                throw new ArgumentNullException(nameof(keyCodeParser));

            _inputSourceType = ParseInputSourceType(settings.Input.InputSourceType);
            _turnLeftKey = keyCodeParser.Parse(settings.Input.TurnLeftKey, "player.input.turnLeftKey");
            _turnRightKey = keyCodeParser.Parse(settings.Input.TurnRightKey, "player.input.turnRightKey");
            _thrustKey = keyCodeParser.Parse(settings.Input.ThrustKey, "player.input.thrustKey");
            _alternativeTurnLeftKey = keyCodeParser.Parse(
                settings.Input.AlternativeTurnLeftKey,
                "player.input.alternativeTurnLeftKey");
            _alternativeTurnRightKey = keyCodeParser.Parse(
                settings.Input.AlternativeTurnRightKey, 
                "player.input.alternativeTurnRightKey");
            _alternativeThrustKey = keyCodeParser.Parse(
                settings.Input.AlternativeThrustKey,
                "player.input.alternativeThrustKey");
            _fireBulletKey = keyCodeParser.Parse(settings.Input.FireBulletKey, "player.input.fireBulletKey");
            _alternativeFireBulletKey = keyCodeParser.Parse(
                settings.Input.AlternativeFireBulletKey,
                "player.input.alternativeFireBulletKey");
            _fireLaserKey = keyCodeParser.Parse(settings.Input.FireLaserKey, "player.input.fireLaserKey");
            _alternativeFireLaserKey = keyCodeParser.Parse(
                settings.Input.AlternativeFireLaserKey,
                "player.input.alternativeFireLaserKey");
        }

        public PlayerInputSourceType InputSourceType => _inputSourceType;
        public KeyCode TurnLeftKey => _turnLeftKey;
        public KeyCode TurnRightKey => _turnRightKey;
        public KeyCode ThrustKey => _thrustKey;
        public KeyCode FireBulletKey => _fireBulletKey;
        public KeyCode FireLaserKey => _fireLaserKey;
        public KeyCode AlternativeTurnLeftKey => _alternativeTurnLeftKey;
        public KeyCode AlternativeTurnRightKey => _alternativeTurnRightKey;
        public KeyCode AlternativeThrustKey => _alternativeThrustKey;
        public KeyCode AlternativeFireBulletKey => _alternativeFireBulletKey;
        public KeyCode AlternativeFireLaserKey => _alternativeFireLaserKey;

        public int PlayerMaxHealth => _settings.Collision.MaxHealth;
        public bool ShowMobileControlsInEditor => _settings.Input.ShowMobileControlsInEditor;
        public float MobileTurnLeftValue => _settings.Input.MobileTurnLeftValue;
        public float MobileTurnRightValue => _settings.Input.MobileTurnRightValue;
        public float PlayerSpawnPositionX => _settings.Movement.SpawnPositionX;
        public float PlayerSpawnPositionY => _settings.Movement.SpawnPositionY;
        public float PlayerAcceleration => _settings.Movement.Acceleration;
        public float PlayerTurnSpeed => _settings.Movement.TurnSpeed;
        public float PlayerMaxSpeed => _settings.Movement.MaxSpeed;
        public float PlayerLinearDamping => _settings.Movement.LinearDamping;
        public float PlayerSpawnRotationDegrees => _settings.Movement.SpawnRotationDegrees;
        public float PlayerCollisionRadius => _settings.Collision.CollisionRadius;
        public float PlayerCollisionBounceSpeed => _settings.Collision.CollisionBounceSpeed;
        public float PlayerInvulnerabilitySeconds => _settings.Collision.InvulnerabilitySeconds;

        private PlayerInputSourceType ParseInputSourceType(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException("player.input.inputSourceType must be assigned in json.");

            if (!Enum.TryParse(value, true, out PlayerInputSourceType sourceType))
                throw new InvalidOperationException($"player.input.inputSourceType has invalid value '{value}'.");

            return sourceType;
        }
    }
}