using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Laser.Contracts;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Signals.Enemies;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Gameplay.Laser.Services
{
    public sealed class PlayerLaserShootingService : ITickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly ILaserSpawnPointProvider _spawnPointProvider;
        private readonly IPlayerControlState _playerControlState;
        private readonly IGameplayPauseState _gameplayPauseState;
        private readonly PlayerLaserSettings _laserSettings;
        private readonly PlayerLaserMagazine _laserMagazine;
        private readonly CollisionBodyRegistry _collisionBodyRegistry;
        private readonly CollisionCategoryPolicy _categoryPolicy;
        private readonly LineCircleIntersectionDetector _lineCircleDetector;
        private readonly SignalBus _signalBus;

        public PlayerLaserShootingService(
            IPlayerInput playerInput,
            ILaserSpawnPointProvider spawnPointProvider,
            IPlayerControlState playerControlState,
            IGameplayPauseState gameplayPauseState,
            PlayerLaserSettings laserSettings,
            PlayerLaserMagazine laserMagazine,
            CollisionBodyRegistry collisionBodyRegistry,
            CollisionCategoryPolicy categoryPolicy,
            LineCircleIntersectionDetector lineCircleDetector,
            SignalBus signalBus)
        {
            _playerInput = playerInput;
            _spawnPointProvider = spawnPointProvider;
            _playerControlState = playerControlState;
            _gameplayPauseState = gameplayPauseState;
            _laserSettings = laserSettings;
            _laserMagazine = laserMagazine;
            _collisionBodyRegistry = collisionBodyRegistry;
            _categoryPolicy = categoryPolicy;
            _lineCircleDetector = lineCircleDetector;
            _signalBus = signalBus;
        }

        void ITickable.Tick()
        {
            if (_gameplayPauseState.IsPaused)
                return;

            if (!_playerControlState.CanControl)
                return;

            PlayerInputState inputState = _playerInput.GetState();

            if (!inputState.IsLaserFirePressed)
                return;

            FireLaser();
        }

        private void FireLaser()
        {
            if (!_laserMagazine.ConsumeCharge())
                return;

            Vector2D startPosition = _spawnPointProvider.Position;
            Vector2D direction = _spawnPointProvider.Direction;
            Vector2D endPosition = startPosition.Add(direction.Multiply(_laserSettings.Length));
            _signalBus.Fire(new PlayerLaserChargesChangedSignal(_laserMagazine.Charges, _laserMagazine.MaxCharges));
            _signalBus.Fire(new PlayerLaserFiredSignal(_laserSettings.VisualWidth, _laserSettings.VisibleSeconds));

            HitEnemiesOnLaserLine(startPosition, endPosition);
        }

        private void HitEnemiesOnLaserLine(Vector2D startPosition, Vector2D endPosition)
        {
            IReadOnlyList<CollisionBody> bodies = _collisionBodyRegistry.Bodies;

            for (int i = 0; i < bodies.Count; i++)
            {
                CollisionBody body = bodies[i];

                if (!body.IsActive)
                    continue;

                if (!_categoryPolicy.IsEnemy(body.Category))
                    continue;

                float hitRadius = body.Radius + _laserSettings.HitHalfWidth;

                if (!_lineCircleDetector.IntersectsSegmentCircle(
                        startPosition,
                        endPosition,
                        body.Body.Position,
                        hitRadius))
                {
                    continue;
                }

                _signalBus.Fire(new EnemyHitByLaserSignal(body));
            }
        }
    }
}