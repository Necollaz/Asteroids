using Zenject;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Gameplay.Laser.Services
{
    public sealed class PlayerLaserShootingService : ITickable
    {
        private readonly IPlayerInput _playerInput;
        private readonly IPlayerControlState _playerControlState;
        private readonly IGameplayPauseState _gameplayPauseState;
        private readonly PlayerLaserSettings _laserSettings;
        private readonly PlayerLaserMagazine _laserMagazine;
        private readonly SignalBus _signalBus;

        public PlayerLaserShootingService(
            IPlayerInput playerInput,
            IPlayerControlState playerControlState,
            IGameplayPauseState gameplayPauseState,
            PlayerLaserSettings laserSettings,
            PlayerLaserMagazine laserMagazine,
            SignalBus signalBus)
        {
            _playerInput = playerInput;
            _playerControlState = playerControlState;
            _gameplayPauseState = gameplayPauseState;
            _laserSettings = laserSettings;
            _laserMagazine = laserMagazine;
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

            _signalBus.Fire(new PlayerLaserChargesChangedSignal(_laserMagazine.Charges, _laserMagazine.MaxCharges));
            _signalBus.Fire(new PlayerLaserFiredSignal(_laserSettings.VisualWidth, _laserSettings.VisibleSeconds));
        }
    }
}