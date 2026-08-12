using Zenject;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Laser.States;

namespace AsteroidGame.Scripts.Presentation.Laser
{
    public sealed class PlayerLaserVisualPresenter : IInitializable, ITickable
    {
        private readonly PlayerLaserView _view;
        private readonly PlayerLaserState _laserState;
        private readonly IGameplayPauseState _gameplayPauseState;
        private readonly PlayerLaserSettings _laserSettings;

        private bool _isVisible;

        public PlayerLaserVisualPresenter(
            PlayerLaserView view,
            PlayerLaserState laserState,
            IGameplayPauseState gameplayPauseState,
            PlayerLaserSettings laserSettings)
        {
            _view = view;
            _laserState = laserState;
            _gameplayPauseState = gameplayPauseState;
            _laserSettings = laserSettings;
        }

        void IInitializable.Initialize()
        {
            _view.Hide();
            _isVisible = false;
        }

        void ITickable.Tick()
        {
            if (_gameplayPauseState.IsPaused)
                return;

            if (!_laserState.IsActive)
            {
                HideIfVisible();
                
                return;
            }

            _view.Show(_laserState.Segment.StartPosition, _laserState.Segment.EndPosition, _laserSettings.VisualWidth);
            _isVisible = true;
        }

        private void HideIfVisible()
        {
            if (!_isVisible)
                return;

            _view.Hide();
            _isVisible = false;
        }
    }
}