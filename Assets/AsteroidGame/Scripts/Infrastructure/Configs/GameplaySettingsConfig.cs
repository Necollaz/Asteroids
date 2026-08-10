using UnityEngine;
using AsteroidGame.Scripts.Domain.Player;
using AsteroidGame.Scripts.Infrastructure.Core;
using AsteroidGame.Scripts.Input;

namespace AsteroidGame.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(
        fileName = nameof(GameplaySettingsConfig),
        menuName = Constants.EditorConfigsPath + nameof(GameplaySettingsConfig))]
    public sealed class GameplaySettingsConfig : 
        ScriptableObject,
        IPlayerMovementSettingsData,
        IPlayerCollisionSettingsData,
        IKeyboardInputSettingsData
    {
        [Header("Keyboard Input")]
        [SerializeField] private KeyCode _turnLeftKey = KeyCode.A;
        [SerializeField] private KeyCode _turnRightKey = KeyCode.D;
        [SerializeField] private KeyCode _thrustKey = KeyCode.W;
        [SerializeField] private KeyCode _alternativeTurnLeftKey = KeyCode.LeftArrow;
        [SerializeField] private KeyCode _alternativeTurnRightKey = KeyCode.RightArrow;
        [SerializeField] private KeyCode _alternativeThrustKey = KeyCode.UpArrow;

        [Header("Player Movement")]
        [SerializeField] private Vector2 _playerSpawnPosition;
        [SerializeField] private float _playerAcceleration = 18f;
        [SerializeField] private float _playerTurnSpeed = 180f;
        [SerializeField] private float _playerMaxSpeed = 12f;
        [SerializeField] private float _playerLinearDamping = 0.15f;
        [SerializeField] private float _playerSpawnRotationDegrees;
        
        [Header("Player Collision")]
        [SerializeField] private int _playerMaxHealth = 3;
        [SerializeField] private float _playerCollisionRadius = 0.45f;
        [SerializeField] private float _playerCollisionBounceSpeed = 8f;
        [SerializeField] private float _playerInvulnerabilitySeconds = 3f;
        
        public KeyCode TurnLeftKey => _turnLeftKey;
        public KeyCode TurnRightKey => _turnRightKey;
        public KeyCode ThrustKey => _thrustKey;
        public KeyCode AlternativeTurnLeftKey => _alternativeTurnLeftKey;
        public KeyCode AlternativeTurnRightKey => _alternativeTurnRightKey;
        public KeyCode AlternativeThrustKey => _alternativeThrustKey;
        public float PlayerSpawnPositionX => _playerSpawnPosition.x;
        public float PlayerSpawnPositionY => _playerSpawnPosition.y;
        public float PlayerAcceleration => _playerAcceleration;
        public float PlayerTurnSpeed => _playerTurnSpeed;
        public float PlayerMaxSpeed => _playerMaxSpeed;
        public float PlayerLinearDamping => _playerLinearDamping;
        public float PlayerSpawnRotationDegrees => _playerSpawnRotationDegrees;
        public int PlayerMaxHealth => _playerMaxHealth;
        public float PlayerCollisionRadius => _playerCollisionRadius;
        public float PlayerCollisionBounceSpeed => _playerCollisionBounceSpeed;
        public float PlayerInvulnerabilitySeconds => _playerInvulnerabilitySeconds;
    }
}