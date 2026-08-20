using Zenject;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Enemies.Settings;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Pooling;
using AsteroidGame.Scripts.Gameplay.Asteroids.Spawning;
using AsteroidGame.Scripts.Gameplay.Enemies.Spawning;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Services
{
    public sealed class AsteroidSpawnService : ITickable
    {
        private const string SpawnFailedMessage =
            "Large asteroid spawn skipped. Asteroid pool has no available instances.";
        
        private readonly AsteroidPool _pool;
        private readonly AsteroidSettings _asteroidSettings;
        private readonly EnemySpawnSettings _enemySettings;
        private readonly AsteroidSpawnPointSelector _spawnPointSelector;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;

        private float _remainingSeconds;

        public AsteroidSpawnService(
            AsteroidPool pool,
            AsteroidSettings asteroidSettings,
            EnemySpawnSettings enemySettings,
            AsteroidSpawnPointSelector spawnPointSelector,
            PhysicsValueFactory physicsValueFactory,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState)
        {
            _pool = pool;
            _asteroidSettings = asteroidSettings;
            _enemySettings = enemySettings;
            _spawnPointSelector = spawnPointSelector;
            _physicsValueFactory = physicsValueFactory;
            _timeProvider = timeProvider;
            _pauseState = pauseState;
            _remainingSeconds = _enemySettings.AsteroidSpawnIntervalSeconds;
        }

        void ITickable.Tick()
        {
            if (_pauseState.IsPaused)
                return;
            
            if (_pool.ActiveAsteroids.Count >= _enemySettings.MaxActiveAsteroids)
                return;
            
            _remainingSeconds -= _timeProvider.DeltaTime;
            
            if (_remainingSeconds > 0f)
                return;

            SpawnLargeAsteroid();
            _remainingSeconds = _enemySettings.AsteroidSpawnIntervalSeconds;
        }

        private void SpawnLargeAsteroid()
        {
            AsteroidSpawnData spawnData = _spawnPointSelector.Select();
            float speed = _asteroidSettings.GetSpeed(EnemyType.LargeAsteroid);
            Velocity velocity = _physicsValueFactory.CreateVelocity(spawnData.Direction.Multiply(speed));
            
            if (_pool.TrySpawn(EnemyType.LargeAsteroid, spawnData.Position, velocity, 0f))
                return;
            
            Debug.LogWarning(SpawnFailedMessage);
        }
    }
}