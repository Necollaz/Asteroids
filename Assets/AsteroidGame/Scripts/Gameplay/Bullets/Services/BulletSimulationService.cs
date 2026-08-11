using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Bullets.Settings;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Pooling;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Services
{
    public sealed class BulletSimulationService : ITickable
    {
        private readonly BulletPool _bulletPool;
        private readonly BulletSettings _settings;
        private readonly WorldBounds _worldBounds;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;

        public BulletSimulationService(
            BulletPool bulletPool,
            BulletSettings settings,
            WorldBounds worldBounds,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState)
        {
            _bulletPool = bulletPool;
            _settings = settings;
            _worldBounds = worldBounds;
            _timeProvider = timeProvider;
            _pauseState = pauseState;
        }

        void ITickable.Tick()
        {
            if (_pauseState.IsPaused)
                return;

            float deltaTime = _timeProvider.DeltaTime;
            IReadOnlyList<BulletInstance> bullets = _bulletPool.ActiveBullets;

            for (int i = bullets.Count - 1; i >= 0; i--)
                TickBullet(bullets[i], deltaTime);
        }

        private void TickBullet(BulletInstance bullet, float deltaTime)
        {
            if (bullet.Model.TickLifetime(deltaTime))
            {
                _bulletPool.Release(bullet);
                
                return;
            }
            
            Body2D body = bullet.Model.Body;
            Vector2D nextPosition = body.Position.Add(body.Velocity.Value.Multiply(deltaTime));
            body.SetPosition(nextPosition);

            if (!_worldBounds.Contains(nextPosition, _settings.VisibilityMargin))
            {
                _bulletPool.Release(bullet);
                
                return;
            }
            
            bullet.RefreshView();
        }
    }
}