using AsteroidGame.Scripts.Domain.Enemies.Settings;
using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Ufo.Settings;
using AsteroidGame.Scripts.Gameplay.Enemies.Spawning;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Gameplay.Ufo.Pooling;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Services
{
    public sealed class UfoSpawnService : ITickable
    {
        private readonly UfoPool _pool;
        private readonly UfoSettings _ufoSettings;
        private readonly EnemySpawnSettings _enemySettings;
        private readonly OutsideWorldSpawnPointSelector _spawnPointSelector;
        private readonly IPlayerBodyProvider _playerBodyProvider;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;
        
        private float _remainingSeconds;

        public UfoSpawnService(
            UfoPool pool,
            UfoSettings ufoSettings,
            EnemySpawnSettings enemySettings,
            OutsideWorldSpawnPointSelector spawnPointSelector,
            IPlayerBodyProvider playerBodyProvider,
            PhysicsValueFactory physicsValueFactory,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState)
        {
            _pool = pool;
            _ufoSettings = ufoSettings;
            _enemySettings = enemySettings;
            _spawnPointSelector = spawnPointSelector;
            _playerBodyProvider = playerBodyProvider;
            _physicsValueFactory = physicsValueFactory;
            _timeProvider = timeProvider;
            _pauseState = pauseState;
            _remainingSeconds = enemySettings.UfoSpawnIntervalSeconds;
        }

        void ITickable.Tick()
        {
            if (_pauseState.IsPaused)
                return;
            
            if (_pool.ActiveUfos.Count >= _enemySettings.MaxActiveUfo)
                return;

            _remainingSeconds -= _timeProvider.DeltaTime;
            
            if (_remainingSeconds > 0f)
                return;

            SpawnUfo();
            _remainingSeconds = _enemySettings.UfoSpawnIntervalSeconds;
        }

        private void SpawnUfo()
        {
            Vector2D position = _spawnPointSelector.Select(_enemySettings.UfoSpawnMargin);
            Vector2D direction = _playerBodyProvider.Body.Position.Subtract(position).Normalized;
            Velocity velocity = _physicsValueFactory.CreateVelocity(direction.Multiply(_ufoSettings.Speed));

            _pool.TrySpawn(position, velocity, 0f);
        }
    }
}