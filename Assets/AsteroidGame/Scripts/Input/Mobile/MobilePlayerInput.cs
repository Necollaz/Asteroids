using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Input.Contracts;
using AsteroidGame.Scripts.Input.Mobile.Views;

namespace AsteroidGame.Scripts.Input.Mobile
{
    public sealed class MobilePlayerInput : IPlayerInputSource
    {
        private readonly MobilePlayerInputView _view;
        private readonly IMobileInputSettingsData _settingsData;

        public MobilePlayerInput(MobilePlayerInputView view, IMobileInputSettingsData settingsData)
        {
            _view = view;
            _settingsData = settingsData;
        }
        
        public PlayerInputSourceType SourceType => PlayerInputSourceType.Mobile;

        public PlayerInputState GetState()
        {
            float turnDirection = 0f;

            if (_view.IsTurnLeftPressed)
                turnDirection = _settingsData.MobileTurnLeftValue;
            
            if (_view.IsTurnRightPressed)
                turnDirection = _settingsData.MobileTurnRightValue;
            
            return new PlayerInputState(
                turnDirection,
                _view.IsMovePressed,
                _view.IsFirePressed,
                _view.ConsumeLaserFirePressedThisFrame());
        }
    }
}