using AsteroidGame.Scripts.Domain.Player.Settings;

namespace AsteroidGame.Scripts.Gameplay.Laser.Calculations
{
    public sealed class PlayerLaserHitArea
    {
        private readonly PlayerLaserSettings _settings;

        public PlayerLaserHitArea(PlayerLaserSettings settings) => _settings = settings;

        public float HalfWidth
        {
            get
            {
                float visualHalfWidth = _settings.VisualWidth * 0.5f;

                return _settings.HitHalfWidth > visualHalfWidth ? _settings.HitHalfWidth : visualHalfWidth;
            }
        }
    }
}