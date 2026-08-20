using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Sections
{
    [Serializable]
    public sealed class SpawnSettingsConfigSection : IEnemySpawnSettingsData
    {
        [Header("Asteroids")]
        [SerializeField] private int _asteroidPoolSize = 32;
        [SerializeField] private int _maxActiveAsteroids = 12;
        [SerializeField] private int _asteroidExplosionPoolSize = 32;
        [SerializeField] private float _asteroidSpawnIntervalSeconds = 2f;
        [SerializeField] private float _asteroidSpawnMargin = 1f;

        [Header("UFO")] 
        [SerializeField] private int _ufoPoolSize = 4;
        [SerializeField] private int _maxActiveUfo = 1;
        [SerializeField] private int _ufoExplosionPoolSize = 10;
        [SerializeField] private float _ufoSpawnIntervalSeconds = 8f;
        [SerializeField] private float _ufoSpawnMargin = 1.5f;
        
        public int AsteroidPoolSize => _asteroidPoolSize;
        public int MaxActiveAsteroids => _maxActiveAsteroids;
        public int UfoPoolSize => _ufoPoolSize;
        public int MaxActiveUfo => _maxActiveUfo;
        public int AsteroidExplosionPoolSize => _asteroidExplosionPoolSize;
        public int UfoExplosionPoolSize => _ufoExplosionPoolSize;
        public float AsteroidSpawnIntervalSeconds => _asteroidSpawnIntervalSeconds;
        public float AsteroidSpawnMargin => _asteroidSpawnMargin;
        public float UfoSpawnIntervalSeconds => _ufoSpawnIntervalSeconds;
        public float UfoSpawnMargin => _ufoSpawnMargin;
    }
}