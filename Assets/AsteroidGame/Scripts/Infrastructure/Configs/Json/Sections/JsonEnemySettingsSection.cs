using System;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Ufo.Contracts;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.World;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections
{
    public sealed class JsonEnemySettingsSection :
        IAsteroidSettingsData,
        IUfoSettingsData,
        IEnemyRewardSettingsData
    {
        private readonly EnemiesSettingsJson _enemies;
        private readonly WorldSettingsJson _world;

        public JsonEnemySettingsSection(EnemiesSettingsJson enemies, WorldSettingsJson world)
        {
            _enemies = enemies ?? throw new ArgumentNullException(nameof(enemies));
            _world = world ?? throw new ArgumentNullException(nameof(world));
        }

        public int AsteroidPoolSize => _world.Spawning.AsteroidPoolSize;
        public int MaxActiveAsteroids => _world.Spawning.MaxActiveAsteroids;
        public int MediumFragmentsPerLarge => _enemies.Asteroids.MediumFragmentsPerLarge;
        public int SmallFragmentsPerMedium => _enemies.Asteroids.SmallFragmentsPerMedium;
        public int LargeAsteroidReward => _enemies.Rewards.LargeAsteroid;
        public int MediumAsteroidReward => _enemies.Rewards.MediumAsteroid;
        public int SmallAsteroidReward => _enemies.Rewards.SmallAsteroid;
        public int UfoReward => _enemies.Rewards.Ufo;
        public int UfoPoolSize => _world.Spawning.UfoPoolSize;
        public int MaxActiveUfo => _world.Spawning.MaxActiveUfo;
        public float AsteroidSpawnIntervalSeconds => _world.Spawning.AsteroidSpawnIntervalSeconds;
        public float AsteroidSpawnMargin => _world.Spawning.AsteroidSpawnMargin;
        public float AsteroidSpeedReturnRate => _enemies.Asteroids.SpeedReturnRate;
        public float LargeAsteroidRadius => _enemies.Asteroids.LargeRadius;
        public float MediumAsteroidRadius => _enemies.Asteroids.MediumRadius;
        public float SmallAsteroidRadius => _enemies.Asteroids.SmallRadius;
        public float LargeAsteroidSpeed => _enemies.Asteroids.LargeSpeed;
        public float MediumAsteroidSpeed => _enemies.Asteroids.MediumSpeed;
        public float SmallAsteroidSpeed => _enemies.Asteroids.SmallSpeed;
        public float UfoSpawnIntervalSeconds => _world.Spawning.UfoSpawnIntervalSeconds;
        public float UfoSpawnMargin => _world.Spawning.UfoSpawnMargin;
        public float UfoSpeed => _enemies.Ufo.Speed;
        public float UfoCollisionRadius => _enemies.Ufo.CollisionRadius;
        public float UfoMaxTiltDegrees => _enemies.Ufo.MaxTiltDegrees;
        public float UfoKnockbackSeconds => _enemies.Ufo.KnockbackSeconds;
        public float UfoKnockbackDamping => _enemies.Ufo.KnockbackDamping;
    }
}