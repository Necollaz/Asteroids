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
        [SerializeField] private int _mediumFragmentsPerLarge = 2;
        [SerializeField] private int _smallFragmentsPerMedium = 2;
        [SerializeField] private float _largeAsteroidRadius = 0.9f;
        [SerializeField] private float _mediumAsteroidRadius = 0.55f;
        [SerializeField] private float _smallAsteroidRadius = 0.3f;
        [SerializeField] private float _largeAsteroidSpeed = 0.5f;
        [SerializeField] private float _mediumAsteroidSpeed = 0.8f;
        [SerializeField] private float _smallAsteroidSpeed = 1.1f;
        [SerializeField] private float _asteroidSpeedReturnRate = 4f;

        [Header("UFO")]
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
        
        public float AsteroidSpeedReturnRate => _asteroidSpeedReturnRate;
        public float LargeAsteroidRadius => _largeAsteroidRadius;
        public float MediumAsteroidRadius => _mediumAsteroidRadius;
        public float SmallAsteroidRadius => _smallAsteroidRadius;
        public float LargeAsteroidSpeed => _largeAsteroidSpeed;
        public float MediumAsteroidSpeed => _mediumAsteroidSpeed;
        public float SmallAsteroidSpeed => _smallAsteroidSpeed;
        public float UfoSpeed => _ufoSpeed;
        public float UfoCollisionRadius => _ufoCollisionRadius;
        public float UfoMaxTiltDegrees => _ufoMaxTiltDegrees;
        public float UfoKnockbackSeconds => _ufoKnockbackSeconds;
        public float UfoKnockbackDamping => _ufoKnockbackDamping;
        
        public int MediumFragmentsPerLarge => _mediumFragmentsPerLarge;
        public int SmallFragmentsPerMedium => _smallFragmentsPerMedium;
        public int LargeAsteroidReward => _largeAsteroidReward;
        public int MediumAsteroidReward => _mediumAsteroidReward;
        public int SmallAsteroidReward => _smallAsteroidReward;
        public int UfoReward => _ufoReward;
    }
}