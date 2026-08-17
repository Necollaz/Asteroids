using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Input.Contracts;
using AsteroidGame.Scripts.Input.Keyboard;
using AsteroidGame.Scripts.Input.Mobile;

namespace AsteroidGame.Scripts.Input.Common
{
    public sealed class PlayerInputRouter : IPlayerInput
    {
        private const int InvalidFrame = -1;
        
        private readonly PlayerInputSourceResolver _sourceResolver;
        private readonly KeyboardPlayerInput _keyboardInput;
        private readonly MobilePlayerInput _mobileInput;
        private readonly IPlayerInputFrameProvider _frameProvider;
        
        private PlayerInputState _cachedState;
        private int _cachedFrame = InvalidFrame;

        public PlayerInputRouter(
            PlayerInputSourceResolver sourceResolver,
            KeyboardPlayerInput keyboardInput,
            MobilePlayerInput mobileInput,
            IPlayerInputFrameProvider frameProvider)
        {
            _sourceResolver = sourceResolver;
            _keyboardInput = keyboardInput;
            _mobileInput = mobileInput;
            _frameProvider = frameProvider;
        }

        public PlayerInputState GetState()
        {
            if (_cachedFrame == _frameProvider.CurrentFrame)
                return _cachedState;

            _cachedState = ReadCurrentSourceState();
            _cachedFrame = _frameProvider.CurrentFrame;
            
            return _cachedState;
        }

        private PlayerInputState ReadCurrentSourceState()
        {
            if (_sourceResolver.Resolve() == PlayerInputSourceType.Mobile)
                return _mobileInput.GetState();
            
            return _keyboardInput.GetState();
        }
    }
}