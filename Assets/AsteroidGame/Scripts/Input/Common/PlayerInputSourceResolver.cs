using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Input.Common
{
    public sealed class PlayerInputSourceResolver
    {
        private readonly IPlayerInputRouterSettingsData _routerSettings;
        private readonly IMobileInputSettingsData _mobileSettings;
        private readonly IPlayerInputPlatform _platform;

        public PlayerInputSourceResolver(
            IPlayerInputRouterSettingsData routerSettings,
            IMobileInputSettingsData mobileSettings,
            IPlayerInputPlatform platform)
        {
            _routerSettings = routerSettings;
            _mobileSettings = mobileSettings;
            _platform = platform;
        }

        public PlayerInputSourceType Resolve()
        {
            if (_routerSettings.InputSourceType != PlayerInputSourceType.Auto)
                return _routerSettings.InputSourceType;
            
            if (_platform.IsMobilePlatform)
                return PlayerInputSourceType.Mobile;

            if (_platform.IsEditor && _mobileSettings.ShowMobileControlsInEditor)
                return PlayerInputSourceType.Mobile;
            
            return PlayerInputSourceType.Keyboard;
        }
    }
}