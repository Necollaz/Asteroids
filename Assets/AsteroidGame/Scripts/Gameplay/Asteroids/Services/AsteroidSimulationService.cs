using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Gameplay.Asteroids.Calculations;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Pooling;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Services
{
    public sealed class AsteroidSimulationService : IFixedTickable
    {
        private readonly AsteroidPool _pool;
        private readonly WorldBounds _worldBounds;
        private readonly AsteroidSettings _settings;
        private readonly AsteroidVelocityStabilizer _velocityStabilizer;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;

        public AsteroidSimulationService(
            AsteroidPool pool,
            WorldBounds worldBounds,
            AsteroidVelocityStabilizer velocityStabilizer,
            AsteroidSettings settings,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState)
        {
            _pool = pool;
            _worldBounds = worldBounds;
            _settings = settings;
            _velocityStabilizer = velocityStabilizer;
            _timeProvider = timeProvider;
            _pauseState = pauseState;
        }

        void IFixedTickable.FixedTick()
        {
            if (_pauseState.IsPaused)
                return;

            float deltaTime = _timeProvider.FixedDeltaTime;
            IReadOnlyList<AsteroidInstance> asteroids = _pool.ActiveAsteroids;

            for (int i = 0; i < asteroids.Count; i++)
                TickAsteroid(asteroids[i], deltaTime);
        }

        private void TickAsteroid(AsteroidInstance asteroid, float deltaTime)
        {
            Body2D body = asteroid.Body;
            _velocityStabilizer.Stabilize(asteroid, deltaTime);
            
            Vector2D nextPosition = body.Position.Add(body.Velocity.Value.Multiply(deltaTime));

            if (!_worldBounds.Contains(nextPosition, _settings.SpawnMargin))
                nextPosition = _worldBounds.WrapPosition(nextPosition);
            
            body.SetPosition(nextPosition);
            asteroid.RefreshView();
        }
    }
}