using System;
using Zenject;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Laser.Calculations;
using AsteroidGame.Scripts.Gameplay.Laser.States;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Gameplay.Laser.Services
{
    public sealed class PlayerLaserActivationService : IInitializable, IDisposable, IFixedTickable
    {
        private readonly PlayerLaserState _laserState;
        private readonly PlayerLaserBeamGeometry _beamGeometry;
        private readonly IGameplayPauseState _gameplayPauseState;
        private readonly ITimeProvider _timeProvider;
        private readonly SignalBus _signalBus;

        public PlayerLaserActivationService(
            PlayerLaserState laserState,
            PlayerLaserBeamGeometry beamGeometry,
            IGameplayPauseState gameplayPauseState,
            ITimeProvider timeProvider,
            SignalBus signalBus)
        {
            _laserState = laserState;
            _beamGeometry = beamGeometry;
            _gameplayPauseState = gameplayPauseState;
            _timeProvider = timeProvider;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize() => _signalBus.Subscribe<PlayerLaserFiredSignal>(HandleLaserFired);

        void IDisposable.Dispose() => _signalBus.Unsubscribe<PlayerLaserFiredSignal>(HandleLaserFired);

        void IFixedTickable.FixedTick()
        {
            if (_gameplayPauseState.IsPaused)
                return;

            if (!_laserState.IsActive)
                return;

            _laserState.RefreshSegment(_beamGeometry.CreateCurrentSegment());
        }

        private void HandleLaserFired(PlayerLaserFiredSignal signal)
        {
            _laserState.Activate(signal.VisibleSeconds);
            _laserState.RefreshSegment(_beamGeometry.CreateCurrentSegment());
        }
    }
}