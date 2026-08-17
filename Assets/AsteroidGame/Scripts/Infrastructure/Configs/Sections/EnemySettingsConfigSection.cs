using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Asteroids.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Ufo.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Sections
{
    [Serializable]
    public sealed class EnemySettingsConfigSection :
        IAsteroidSettingsData,
        IUfoSettingsData,
        IEnemyRewardSettingsData
    {
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

        [Header("Rewards")]
        [SerializeField] private int _largeAsteroidReward = 20;
        [SerializeField] private int _mediumAsteroidReward = 50;
        [SerializeField] private int _smallAsteroidReward = 100;
        [SerializeField] private int _ufoReward = 200;

        public float AsteroidSpawnIntervalSeconds => _asteroidSpawnIntervalSeconds;
        public float AsteroidSpawnMargin => _asteroidSpawnMargin;
        public float AsteroidSpeedReturnRate => _asteroidSpeedReturnRate;
        public float LargeAsteroidRadius => _largeAsteroidRadius;
        public float MediumAsteroidRadius => _mediumAsteroidRadius;
        public float SmallAsteroidRadius => _smallAsteroidRadius;
        public float LargeAsteroidSpeed => _largeAsteroidSpeed;
        public float MediumAsteroidSpeed => _mediumAsteroidSpeed;
        public float SmallAsteroidSpeed => _smallAsteroidSpeed;
        public float UfoSpawnIntervalSeconds => _ufoSpawnIntervalSeconds;
        public float UfoSpawnMargin => _ufoSpawnMargin;
        public float UfoSpeed => _ufoSpeed;
        public float UfoCollisionRadius => _ufoCollisionRadius;
        public float UfoMaxTiltDegrees => _ufoMaxTiltDegrees;
        public float UfoKnockbackSeconds => _ufoKnockbackSeconds;
        public float UfoKnockbackDamping => _ufoKnockbackDamping;
        public int AsteroidPoolSize => _asteroidPoolSize;
        public int MaxActiveAsteroids => _maxActiveAsteroids;
        public int MediumFragmentsPerLarge => _mediumFragmentsPerLarge;
        public int SmallFragmentsPerMedium => _smallFragmentsPerMedium;
        public int UfoPoolSize => _ufoPoolSize;
        public int MaxActiveUfo => _maxActiveUfo;
        public int LargeAsteroidReward => _largeAsteroidReward;
        public int MediumAsteroidReward => _mediumAsteroidReward;
        public int SmallAsteroidReward => _smallAsteroidReward;
        public int UfoReward => _ufoReward;
    }
}