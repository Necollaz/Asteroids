using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Sections
{
    [Serializable]
    public sealed class PlayerSettingsConfigSection :
        IPlayerMovementSettingsData,
        IPlayerCollisionSettingsData,
        IKeyboardInputSettingsData,
        IPlayerInputRouterSettingsData,
        IMobileInputSettingsData
    {
        [Header("Input Source")]
        [SerializeField] private PlayerInputSourceType _inputSourceType = PlayerInputSourceType.Auto;
        [SerializeField] private bool _showMobileControlsInEditor;
        [SerializeField] private float _mobileTurnLeftValue = 1f;
        [SerializeField] private float _mobileTurnRightValue = -1f;

        [Header("Keyboard Input")]
        [SerializeField] private KeyCode _turnLeftKey = KeyCode.A;
        [SerializeField] private KeyCode _turnRightKey = KeyCode.D;
        [SerializeField] private KeyCode _thrustKey = KeyCode.W;
        [SerializeField] private KeyCode _alternativeTurnLeftKey = KeyCode.LeftArrow;
        [SerializeField] private KeyCode _alternativeTurnRightKey = KeyCode.RightArrow;
        [SerializeField] private KeyCode _alternativeThrustKey = KeyCode.UpArrow;
        [SerializeField] private KeyCode _fireBulletKey = KeyCode.Space;
        [SerializeField] private KeyCode _alternativeFireBulletKey = KeyCode.Mouse0;
        [SerializeField] private KeyCode _fireLaserKey = KeyCode.E;
        [SerializeField] private KeyCode _alternativeFireLaserKey = KeyCode.Mouse1;

        [Header("Movement")]
        [SerializeField] private Vector2 _spawnPosition;
        [SerializeField] private float _acceleration = 18f;
        [SerializeField] private float _turnSpeed = 180f;
        [SerializeField] private float _maxSpeed = 12f;
        [SerializeField] private float _linearDamping = 0.15f;
        [SerializeField] private float _spawnRotationDegrees;

        [Header("Collision")]
        [SerializeField] private int _maxHealth = 3;
        [SerializeField] private float _collisionRadius = 0.45f;
        [SerializeField] private float _collisionBounceSpeed = 8f;
        [SerializeField] private float _invulnerabilitySeconds = 3f;

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

        public bool ShowMobileControlsInEditor => _showMobileControlsInEditor;
        public float MobileTurnLeftValue => _mobileTurnLeftValue;
        public float MobileTurnRightValue => _mobileTurnRightValue;
        public float PlayerSpawnPositionX => _spawnPosition.x;
        public float PlayerSpawnPositionY => _spawnPosition.y;
        public float PlayerAcceleration => _acceleration;
        public float PlayerTurnSpeed => _turnSpeed;
        public float PlayerMaxSpeed => _maxSpeed;
        public float PlayerLinearDamping => _linearDamping;
        public float PlayerSpawnRotationDegrees => _spawnRotationDegrees;
        public float PlayerCollisionRadius => _collisionRadius;
        public float PlayerCollisionBounceSpeed => _collisionBounceSpeed;
        public float PlayerInvulnerabilitySeconds => _invulnerabilitySeconds;
        public int PlayerMaxHealth => _maxHealth;
    }
}