using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Domain.Ufo.Contracts;
using AsteroidGame.Scripts.Domain.World;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.World;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Loading;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json
{
    public sealed class GameplayJsonSettings :
        IPlayerMovementSettingsData,
        IPlayerCollisionSettingsData,
        IPlayerLaserSettingsData,
        IKeyboardInputSettingsData,
        IPlayerInputRouterSettingsData,
        IMobileInputSettingsData,
        IBulletSettingsData,
        IAsteroidSettingsData,
        IEnemyRewardSettingsData,
        IUfoSettingsData,
        IWorldSettingsData
    {
        private readonly PlayerSettingsJson _player;
        private readonly EnemiesSettingsJson _enemies;
        private readonly WorldSettingsJson _world;

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

        public GameplayJsonSettings(
            PlayerSettingsJson player,
            EnemiesSettingsJson enemies,
            WorldSettingsJson world,
            KeyCodeParser keyCodeParser)
        {
            _player = player;
            _enemies = enemies;
            _world = world;

            _inputSourceType = ParseInputSourceType(player.Input.InputSourceType);
            _turnLeftKey = keyCodeParser.Parse(player.Input.TurnLeftKey, "player.input.turnLeftKey");
            _turnRightKey = keyCodeParser.Parse(player.Input.TurnRightKey, "player.input.turnRightKey");
            _thrustKey = keyCodeParser.Parse(player.Input.ThrustKey, "player.input.thrustKey");
            _alternativeTurnLeftKey = keyCodeParser.Parse(
                player.Input.AlternativeTurnLeftKey,
                "player.input.alternativeTurnLeftKey");
            _alternativeTurnRightKey = keyCodeParser.Parse(
                player.Input.AlternativeTurnRightKey, 
                "player.input.alternativeTurnRightKey");
            _alternativeThrustKey = keyCodeParser.Parse(
                player.Input.AlternativeThrustKey, 
                "player.input.alternativeThrustKey");
            _fireBulletKey = keyCodeParser.Parse(player.Input.FireBulletKey, "player.input.fireBulletKey");
            _alternativeFireBulletKey = keyCodeParser.Parse(
                player.Input.AlternativeFireBulletKey,
                "player.input.alternativeFireBulletKey");
            _fireLaserKey = keyCodeParser.Parse(player.Input.FireLaserKey, "player.input.fireLaserKey");
            _alternativeFireLaserKey = keyCodeParser.Parse(
                player.Input.AlternativeFireLaserKey,
                "player.input.alternativeFireLaserKey");
        }

        public PlayerInputSourceType InputSourceType => _inputSourceType;
        public bool ShowMobileControlsInEditor => _player.Input.ShowMobileControlsInEditor;
        public float MobileTurnLeftValue => _player.Input.MobileTurnLeftValue;
        public float MobileTurnRightValue => _player.Input.MobileTurnRightValue;

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

        public float PlayerSpawnPositionX => _player.Movement.SpawnPositionX;
        public float PlayerSpawnPositionY => _player.Movement.SpawnPositionY;
        public float PlayerAcceleration => _player.Movement.Acceleration;
        public float PlayerTurnSpeed => _player.Movement.TurnSpeed;
        public float PlayerMaxSpeed => _player.Movement.MaxSpeed;
        public float PlayerLinearDamping => _player.Movement.LinearDamping;
        public float PlayerSpawnRotationDegrees => _player.Movement.SpawnRotationDegrees;

        public int PlayerMaxHealth => _player.Collision.MaxHealth;
        public float PlayerCollisionRadius => _player.Collision.CollisionRadius;
        public float PlayerCollisionBounceSpeed => _player.Collision.CollisionBounceSpeed;
        public float PlayerInvulnerabilitySeconds => _player.Collision.InvulnerabilitySeconds;

        public int PlayerMaxLaserCharges => _player.Laser.MaxCharges;
        public int PlayerInitialLaserCharges => _player.Laser.InitialCharges;
        public float PlayerLaserRechargeSeconds => _player.Laser.RechargeSeconds;
        public float PlayerLaserVisibleSeconds => _player.Laser.VisibleSeconds;
        public float PlayerLaserLength => _player.Laser.Length;
        public float PlayerLaserHitHalfWidth => _player.Laser.HitHalfWidth;
        public float PlayerLaserVisualWidth => _player.Laser.VisualWidth;

        public float BulletSpeed => _player.Bullets.Speed;
        public float BulletLifetimeSeconds => _player.Bullets.LifetimeSeconds;
        public float BulletRadius => _player.Bullets.Radius;
        public float BulletShotsPerSecond => _player.Bullets.ShotsPerSecond;
        public float BulletSpawnOffset => _player.Bullets.SpawnOffset;
        public float BulletVisibilityMargin => _player.Bullets.VisibilityMargin;
        public int PoolSize => _player.Bullets.PoolSize;

        public float AsteroidSpawnIntervalSeconds => _world.Spawning.AsteroidSpawnIntervalSeconds;
        public float AsteroidSpawnMargin => _world.Spawning.AsteroidSpawnMargin;
        public float AsteroidSpeedReturnRate => _enemies.Asteroids.SpeedReturnRate;
        public float LargeAsteroidRadius => _enemies.Asteroids.LargeRadius;
        public float MediumAsteroidRadius => _enemies.Asteroids.MediumRadius;
        public float SmallAsteroidRadius => _enemies.Asteroids.SmallRadius;
        public float LargeAsteroidSpeed => _enemies.Asteroids.LargeSpeed;
        public float MediumAsteroidSpeed => _enemies.Asteroids.MediumSpeed;
        public float SmallAsteroidSpeed => _enemies.Asteroids.SmallSpeed;
        public int AsteroidPoolSize => _world.Spawning.AsteroidPoolSize;
        public int MaxActiveAsteroids => _world.Spawning.MaxActiveAsteroids;
        public int MediumFragmentsPerLarge => _enemies.Asteroids.MediumFragmentsPerLarge;
        public int SmallFragmentsPerMedium => _enemies.Asteroids.SmallFragmentsPerMedium;

        public int UfoPoolSize => _world.Spawning.UfoPoolSize;
        public int MaxActiveUfo => _world.Spawning.MaxActiveUfo;
        public float UfoSpawnIntervalSeconds => _world.Spawning.UfoSpawnIntervalSeconds;
        public float UfoSpawnMargin => _world.Spawning.UfoSpawnMargin;
        public float UfoSpeed => _enemies.Ufo.Speed;
        public float UfoCollisionRadius => _enemies.Ufo.CollisionRadius;
        public float UfoMaxTiltDegrees => _enemies.Ufo.MaxTiltDegrees;
        public float UfoKnockbackSeconds => _enemies.Ufo.KnockbackSeconds;
        public float UfoKnockbackDamping => _enemies.Ufo.KnockbackDamping;

        public int LargeAsteroidReward => _enemies.Rewards.LargeAsteroid;
        public int MediumAsteroidReward => _enemies.Rewards.MediumAsteroid;
        public int SmallAsteroidReward => _enemies.Rewards.SmallAsteroid;
        public int UfoReward => _enemies.Rewards.Ufo;

        public float WorldWidth => _world.World.Width;
        public float WorldHeight => _world.World.Height;

        private PlayerInputSourceType ParseInputSourceType(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException("Player.Input.InputSourceType must be assigned in json.");

            if (!Enum.TryParse(value, true, out PlayerInputSourceType sourceType))
                throw new InvalidOperationException($"Player.Input.InputSourceType has invalid value '{value}'.");

            return sourceType;
        }
    }
}