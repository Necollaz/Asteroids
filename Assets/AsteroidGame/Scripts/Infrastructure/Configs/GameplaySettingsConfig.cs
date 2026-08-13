using UnityEngine;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Domain.Ufo.Contracts;
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
        IPlayerLaserSettingsData,
        IKeyboardInputSettingsData,
        IBulletSettingsData,
        IAsteroidSettingsData,
        IEnemyRewardSettingsData,
        IUfoSettingsData
    {
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
        
        [Header("Bullets")]
        [SerializeField] private int _bulletPoolSize = 24;
        [SerializeField] private float _bulletSpeed = 24f;
        [SerializeField] private float _bulletLifetimeSeconds = 1.2f;
        [SerializeField] private float _bulletRadius = 0.2f;
        [SerializeField] private float _bulletShotsPerSecond = 5f;
        [SerializeField] private float _bulletSpawnOffset = 0.7f;
        [SerializeField] private float _bulletVisibilityMargin = 0.25f;
        
        [Header("Player Laser")]
        [SerializeField] private int _playerMaxLaserCharges = 3;
        [SerializeField] private int _playerInitialLaserCharges = 3;
        [SerializeField] private float _playerLaserRechargeSeconds = 4f;
        [SerializeField] private float _playerLaserVisibleSeconds = 1f;
        [SerializeField] private float _playerLaserLength = 2.5f;
        [SerializeField] private float _playerLaserHitHalfWidth = 0.15f;
        [SerializeField] private float _playerLaserVisualWidth = 0.25f;
        
        [Header("Asteroids")]
        [SerializeField] private int _asteroidPoolSize = 32;
        [SerializeField] private int _maxActiveAsteroids = 12;
        [SerializeField] private int _mediumFragmentsPerLarge = 2;
        [SerializeField] private int _smallFragmentsPerMedium = 2;
        [SerializeField] private float _asteroidSpawnIntervalSeconds = 2f;
        [SerializeField] private float _asteroidSpawnMargin = 1f;
        [SerializeField] private float _largeAsteroidRadius = 0.9f;
        [SerializeField] private float _mediumAsteroidRadius = 0.55f;
        [SerializeField] private float _smallAsteroidRadius = 0.3f;
        [SerializeField] private float _largeAsteroidSpeed = 0.5f;
        [SerializeField] private float _mediumAsteroidSpeed = 0.8f;
        [SerializeField] private float _smallAsteroidSpeed = 1.1f;
        [SerializeField] private float _asteroidSpeedReturnRate = 4f;
        
        [Header("UFO")]
        [SerializeField] private int _ufoPoolSize = 4;
        [SerializeField] private int _maxActiveUfo = 1;
        [SerializeField] private float _ufoSpawnIntervalSeconds = 8f;
        [SerializeField] private float _ufoSpawnMargin = 1.5f;
        [SerializeField] private float _ufoSpeed = 1.4f;
        [SerializeField] private float _ufoCollisionRadius = 0.45f;
        [SerializeField] private float _ufoMaxTiltDegrees = 15f;
        [SerializeField] private float _ufoKnockbackSeconds = 0.6f;
        [SerializeField] private float _ufoKnockbackDamping = 4f;

        [Header("Enemy Rewards")]
        [SerializeField] private int _largeAsteroidReward = 20;
        [SerializeField] private int _mediumAsteroidReward = 50;
        [SerializeField] private int _smallAsteroidReward = 100;
        [SerializeField] private int _ufoReward = 200;
        
        public KeyCode TurnLeftKey => _turnLeftKey;
        public KeyCode TurnRightKey => _turnRightKey;
        public KeyCode ThrustKey => _thrustKey;
        public KeyCode AlternativeTurnLeftKey => _alternativeTurnLeftKey;
        public KeyCode AlternativeTurnRightKey => _alternativeTurnRightKey;
        public KeyCode AlternativeThrustKey => _alternativeThrustKey;
        public KeyCode FireBulletKey => _fireBulletKey;
        public KeyCode AlternativeFireBulletKey => _alternativeFireBulletKey;
        public KeyCode FireLaserKey => _fireLaserKey;
        public KeyCode AlternativeFireLaserKey => _alternativeFireLaserKey;
        
        public float PlayerSpawnPositionX => _playerSpawnPosition.x;
        public float PlayerSpawnPositionY => _playerSpawnPosition.y;
        public float PlayerAcceleration => _playerAcceleration;
        public float PlayerTurnSpeed => _playerTurnSpeed;
        public float PlayerMaxSpeed => _playerMaxSpeed;
        public float PlayerLinearDamping => _playerLinearDamping;
        public float PlayerSpawnRotationDegrees => _playerSpawnRotationDegrees;
        public float PlayerCollisionRadius => _playerCollisionRadius;
        public float PlayerCollisionBounceSpeed => _playerCollisionBounceSpeed;
        public float PlayerInvulnerabilitySeconds => _playerInvulnerabilitySeconds;

        public float BulletSpeed => _bulletSpeed;
        public float BulletLifetimeSeconds => _bulletLifetimeSeconds;
        public float BulletRadius => _bulletRadius;
        public float BulletShotsPerSecond => _bulletShotsPerSecond;
        public float BulletSpawnOffset => _bulletSpawnOffset;
        public float BulletVisibilityMargin => _bulletVisibilityMargin;
        
        public float PlayerLaserRechargeSeconds => _playerLaserRechargeSeconds;
        public float PlayerLaserVisibleSeconds => _playerLaserVisibleSeconds;
        public float PlayerLaserLength => _playerLaserLength;
        public float PlayerLaserHitHalfWidth => _playerLaserHitHalfWidth;
        public float PlayerLaserVisualWidth => _playerLaserVisualWidth;
        
        public float AsteroidSpawnIntervalSeconds => _asteroidSpawnIntervalSeconds;
        public float AsteroidSpawnMargin => _asteroidSpawnMargin;
        public float LargeAsteroidRadius => _largeAsteroidRadius;
        public float MediumAsteroidRadius => _mediumAsteroidRadius;
        public float SmallAsteroidRadius => _smallAsteroidRadius;
        public float LargeAsteroidSpeed => _largeAsteroidSpeed;
        public float MediumAsteroidSpeed => _mediumAsteroidSpeed;
        public float SmallAsteroidSpeed => _smallAsteroidSpeed;
        public float AsteroidSpeedReturnRate => _asteroidSpeedReturnRate;

        public float UfoSpawnIntervalSeconds => _ufoSpawnIntervalSeconds;
        public float UfoSpawnMargin => _ufoSpawnMargin;
        public float UfoSpeed => _ufoSpeed;
        public float UfoCollisionRadius => _ufoCollisionRadius;
        public float UfoMaxTiltDegrees => _ufoMaxTiltDegrees;
        public float UfoKnockbackSeconds => _ufoKnockbackSeconds;
        public float UfoKnockbackDamping => _ufoKnockbackDamping;
        
        public int UfoPoolSize => _ufoPoolSize;
        public int MaxActiveUfo => _maxActiveUfo;
        public int AsteroidPoolSize => _asteroidPoolSize;
        public int MaxActiveAsteroids => _maxActiveAsteroids;
        public int MediumFragmentsPerLarge => _mediumFragmentsPerLarge;
        public int SmallFragmentsPerMedium => _smallFragmentsPerMedium;
        public int LargeAsteroidReward => _largeAsteroidReward;
        public int MediumAsteroidReward => _mediumAsteroidReward;
        public int SmallAsteroidReward => _smallAsteroidReward;
        public int UfoReward => _ufoReward;
        public int PoolSize => _bulletPoolSize;
        public int PlayerMaxHealth => _playerMaxHealth;
        public int PlayerMaxLaserCharges => _playerMaxLaserCharges;
        public int PlayerInitialLaserCharges => _playerInitialLaserCharges;
    }
}