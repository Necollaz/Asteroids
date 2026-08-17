using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Input.Keyboard
{
    public sealed class KeyboardPlayerInput : IPlayerInputSource
    {
        private readonly IKeyboardInputSettingsData _settingsData;
        private readonly IKeyboardInputReader _inputReader;
        
        public KeyboardPlayerInput(IKeyboardInputSettingsData settingsData, IKeyboardInputReader inputReader)
        {
            _settingsData = settingsData;
            _inputReader = inputReader;
        }

        public PlayerInputSourceType SourceType => PlayerInputSourceType.Keyboard;
        
        public PlayerInputState GetState()
        {
            float turnDirection = 0f;

            if (_inputReader.IsHeld(_settingsData.TurnLeftKey) ||
                _inputReader.IsHeld(_settingsData.AlternativeTurnLeftKey))
            {
                turnDirection = 1f;
            }

            if (_inputReader.IsHeld(_settingsData.TurnRightKey) ||
                _inputReader.IsHeld(_settingsData.AlternativeTurnRightKey))
            {
                turnDirection = -1f;
            }

            bool isThrustPressed =
                _inputReader.IsHeld(_settingsData.ThrustKey) ||
                _inputReader.IsHeld(_settingsData.AlternativeThrustKey);
            bool isBulletFirePressed =
                _inputReader.IsHeld(_settingsData.FireBulletKey) ||
                _inputReader.IsHeld(_settingsData.AlternativeFireBulletKey);
            bool isLaserFirePressed =
                _inputReader.IsPressedThisFrame(_settingsData.FireLaserKey) ||
                _inputReader.IsPressedThisFrame(_settingsData.AlternativeFireLaserKey);

            return new PlayerInputState(turnDirection, isThrustPressed, isBulletFirePressed, isLaserFirePressed);
        }
    }
}