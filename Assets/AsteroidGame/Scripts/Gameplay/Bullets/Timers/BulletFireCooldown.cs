using AsteroidGame.Scripts.Domain.Bullets.Settings;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Timers
{
    public sealed class BulletFireCooldown
    {
        private readonly BulletSettings _settings;

        private float _remainingSeconds;

        public BulletFireCooldown(BulletSettings settings) => _settings = settings;

        public bool IsReady => _remainingSeconds <= 0f;

        public void Tick(float deltaTime)
        {
            if (_remainingSeconds <= 0f)
                return;

            _remainingSeconds -= deltaTime;
        }

        public void Restart() => _remainingSeconds = _settings.FireCooldownSeconds;
    }
}