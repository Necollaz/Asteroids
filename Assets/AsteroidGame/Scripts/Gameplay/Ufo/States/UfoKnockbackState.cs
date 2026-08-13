using System;

namespace AsteroidGame.Scripts.Gameplay.Ufo.States
{
    public sealed class UfoKnockbackState
    {
        private float _remainingSeconds;
        
        public bool IsActive => _remainingSeconds > 0f;

        public void Activate(float durationSeconds)
        {
            if (durationSeconds <= 0f)
                throw new ArgumentOutOfRangeException(nameof(durationSeconds));

            _remainingSeconds = durationSeconds;
        }

        public void Tick(float deltaSeconds)
        {
            if (_remainingSeconds <= 0f)
                return;
            
            _remainingSeconds = Math.Max(0f, _remainingSeconds - deltaSeconds);
        }
        
        public void Reset() => _remainingSeconds = 0f;
    }
}