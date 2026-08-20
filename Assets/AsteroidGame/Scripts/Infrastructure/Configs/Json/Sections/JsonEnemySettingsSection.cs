using System;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Ufo.Contracts;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections
{
    public sealed class JsonEnemySettingsSection :
        IAsteroidSettingsData,
        IUfoSettingsData,
        IEnemyRewardSettingsData
    {
        private readonly EnemiesSettingsJson _enemies;

        public JsonEnemySettingsSection(EnemiesSettingsJson enemies) =>
            _enemies = enemies ?? throw new ArgumentNullException(nameof(enemies));

        public int MediumFragmentsPerLarge => _enemies.Asteroids.MediumFragmentsPerLarge;
        public int SmallFragmentsPerMedium => _enemies.Asteroids.SmallFragmentsPerMedium;
        public int LargeAsteroidReward => _enemies.Rewards.LargeAsteroid;
        public int MediumAsteroidReward => _enemies.Rewards.MediumAsteroid;
        public int SmallAsteroidReward => _enemies.Rewards.SmallAsteroid;
        public int UfoReward => _enemies.Rewards.Ufo;
        public float AsteroidSpeedReturnRate => _enemies.Asteroids.SpeedReturnRate;
        public float LargeAsteroidRadius => _enemies.Asteroids.LargeRadius;
        public float MediumAsteroidRadius => _enemies.Asteroids.MediumRadius;
        public float SmallAsteroidRadius => _enemies.Asteroids.SmallRadius;
        public float LargeAsteroidSpeed => _enemies.Asteroids.LargeSpeed;
        public float MediumAsteroidSpeed => _enemies.Asteroids.MediumSpeed;
        public float SmallAsteroidSpeed => _enemies.Asteroids.SmallSpeed;
        public float UfoSpeed => _enemies.Ufo.Speed;
        public float UfoCollisionRadius => _enemies.Ufo.CollisionRadius;
        public float UfoMaxTiltDegrees => _enemies.Ufo.MaxTiltDegrees;
        public float UfoKnockbackSeconds => _enemies.Ufo.KnockbackSeconds;
        public float UfoKnockbackDamping => _enemies.Ufo.KnockbackDamping;
    }
}