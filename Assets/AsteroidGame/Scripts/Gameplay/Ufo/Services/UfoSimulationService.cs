using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Ufo.Settings;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Gameplay.Ufo.Calculations;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Pooling;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Services
{
    public sealed class UfoSimulationService : IFixedTickable
    {
        private readonly UfoPool _pool;
        private readonly UfoSettings _settings;
        private readonly UfoTiltCalculator _tiltCalculator;
        private readonly UfoKnockbackMovement _knockbackMovement;
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly IPlayerBodyProvider _playerBodyProvider;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;

        public UfoSimulationService(
            UfoPool pool,
            UfoSettings settings,
            IPlayerBodyProvider playerBodyProvider,
            PhysicsValueFactory physicsValueFactory,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState,
            UfoTiltCalculator tiltCalculator,
            UfoKnockbackMovement knockbackMovement)
        {
            _pool = pool;
            _settings = settings;
            _playerBodyProvider = playerBodyProvider;
            _physicsValueFactory = physicsValueFactory;
            _timeProvider = timeProvider;
            _pauseState = pauseState;
            _tiltCalculator = tiltCalculator;
            _knockbackMovement = knockbackMovement;
        }

        void IFixedTickable.FixedTick()
        {
            if (_pauseState.IsPaused)
                return;
            
            float deltaTime = _timeProvider.FixedDeltaTime;
            IReadOnlyList<UfoInstance> activeUfo = _pool.ActiveUfo;

            for (int i = 0; i < activeUfo.Count; i++)
                TickUfo(activeUfo[i], deltaTime);
        }

        private void TickUfo(UfoInstance ufo, float deltaTime)
        {
            if (_knockbackMovement.TryMove(ufo, deltaTime, out Vector2D knockbackDirection))
            {
                ufo.Body.SetRotation(_tiltCalculator.Calculate(knockbackDirection));
                ufo.RefreshView();

                return;
            }

            MoveToPlayer(ufo, deltaTime);
        }

        private void MoveToPlayer(UfoInstance ufo, float deltaTime)
        {
            Body2D body = ufo.Body;
            Vector2D direction = _playerBodyProvider.Body.Position.Subtract(body.Position).Normalized;
            Vector2D velocityValue = direction.Multiply(_settings.Speed);
            Velocity velocity = _physicsValueFactory.CreateVelocity(velocityValue);
            Vector2D nextPosition = body.Position.Add(velocityValue.Multiply(deltaTime));
            body.SetVelocity(velocity);
            body.SetPosition(nextPosition);
            body.SetRotation(_tiltCalculator.Calculate(direction));
            ufo.RefreshView();
        }
    }
}