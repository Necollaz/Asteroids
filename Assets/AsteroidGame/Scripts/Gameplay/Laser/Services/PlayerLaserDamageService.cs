using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Laser.Calculations;
using AsteroidGame.Scripts.Gameplay.Laser.Models;
using AsteroidGame.Scripts.Gameplay.Laser.States;
using AsteroidGame.Scripts.Signals.Enemies;

namespace AsteroidGame.Scripts.Gameplay.Laser.Services
{
    public sealed class PlayerLaserDamageService : IFixedTickable
    {
        private const int MaxHitPassesPerTick = 8;
        
        private readonly PlayerLaserState _laserState;
        private readonly IGameplayPauseState _gameplayPauseState;
        private readonly PlayerLaserHitArea _hitArea;
        private readonly CollisionBodyRegistry _collisionBodyRegistry;
        private readonly CollisionCategoryPolicy _collisionCategoryPolicy;
        private readonly LineCircleIntersectionDetector _lineCircleDetector;
        private readonly SignalBus _signalBus;
        private readonly List<CollisionBody> _hitBuffer = new();

        public PlayerLaserDamageService(
            PlayerLaserState laserState,
            IGameplayPauseState gameplayPauseState,
            PlayerLaserHitArea hitArea,
            CollisionBodyRegistry collisionBodyRegistry,
            CollisionCategoryPolicy collisionCategoryPolicy,
            LineCircleIntersectionDetector lineCircleDetector,
            SignalBus signalBus)
        {
            _laserState = laserState;
            _gameplayPauseState = gameplayPauseState;
            _hitArea = hitArea;
            _collisionBodyRegistry = collisionBodyRegistry;
            _collisionCategoryPolicy = collisionCategoryPolicy;
            _lineCircleDetector = lineCircleDetector;
            _signalBus = signalBus;
        }

        void IFixedTickable.FixedTick()
        {
            if (_gameplayPauseState.IsPaused)
                return;

            if (!_laserState.IsActive)
                return;

            HitEnemiesOnLaserSegment(_laserState.Segment);
        }
        
        private void HitEnemiesOnLaserSegment(PlayerLaserBeamSegment segment)
        {
            for (int pass = 0; pass < MaxHitPassesPerTick; pass++)
            {
                CollectHits(segment);

                if (_hitBuffer.Count == 0)
                    return;

                for (int i = 0; i < _hitBuffer.Count; i++)
                    _signalBus.Fire(new EnemyHitByLaserSignal(_hitBuffer[i]));

                _hitBuffer.Clear();
            }
        }

        private void CollectHits(PlayerLaserBeamSegment segment)
        {
            _hitBuffer.Clear();

            IReadOnlyList<CollisionBody> bodies = _collisionBodyRegistry.Bodies;
            float halfWidth = _hitArea.HalfWidth;

            for (int i = 0; i < bodies.Count; i++)
            {
                CollisionBody body = bodies[i];

                if (!body.IsActive)
                    continue;

                if (!_collisionCategoryPolicy.IsEnemy(body.Category))
                    continue;

                float hitRadius = body.Radius + halfWidth;

                if (!_lineCircleDetector.IntersectsSegmentCircle(
                        segment.StartPosition,
                        segment.EndPosition,
                        body.Body.Position,
                        hitRadius))
                {
                    continue;
                }

                _hitBuffer.Add(body);
            }
        }
    }
}