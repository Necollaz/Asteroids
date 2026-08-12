using Zenject;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Laser.States;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.Gameplay.Laser.Services
{
    public sealed class PlayerLaserLifetimeService : IFixedTickable
    {
        private readonly PlayerLaserState _laserState;
        private readonly IGameplayPauseState _gameplayPauseState;
        private readonly ITimeProvider _timeProvider;

        public PlayerLaserLifetimeService(
            PlayerLaserState laserState,
            IGameplayPauseState gameplayPauseState,
            ITimeProvider timeProvider)
        {
            _laserState = laserState;
            _gameplayPauseState = gameplayPauseState;
            _timeProvider = timeProvider;
        }

        void IFixedTickable.FixedTick()
        {
            if (_gameplayPauseState.IsPaused)
                return;

            _laserState.ReduceRemainingTime(_timeProvider.FixedDeltaTime);
        }
    }
}