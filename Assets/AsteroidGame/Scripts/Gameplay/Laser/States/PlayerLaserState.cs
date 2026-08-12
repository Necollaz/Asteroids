using System;
using AsteroidGame.Scripts.Gameplay.Laser.Models;

namespace AsteroidGame.Scripts.Gameplay.Laser.States
{
    public sealed class PlayerLaserState
    {
        private PlayerLaserBeamSegment _segment;
        
        public PlayerLaserBeamSegment Segment
        {
            get
            {
                if (!IsActive)
                    throw new InvalidOperationException("Laser segment is not available while laser is inactive.");

                return _segment;
            }
        }
        public bool IsActive { get; private set; }
        public float RemainingSeconds { get; private set; }
        
        public void Activate(float durationSeconds)
        {
            if (durationSeconds <= 0f)
                throw new ArgumentOutOfRangeException(nameof(durationSeconds));

            RemainingSeconds = durationSeconds;
            IsActive = true;
        }

        public void RefreshSegment(PlayerLaserBeamSegment segment) => _segment = segment;

        public void ReduceRemainingTime(float deltaTime)
        {
            if (!IsActive)
                return;

            RemainingSeconds -= deltaTime;

            if (RemainingSeconds <= 0f)
                Deactivate();
        }

        public void Deactivate()
        {
            RemainingSeconds = 0f;
            IsActive = false;
        }
    }
}