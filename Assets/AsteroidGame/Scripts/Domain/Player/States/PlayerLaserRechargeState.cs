using System;

namespace AsteroidGame.Scripts.Domain.Player.States
{
    public sealed class PlayerLaserRechargeState
    {
        public bool IsRecharging  { get; private set; }
        public float DurationSeconds { get; private set; }
        public float RemainingSeconds { get; private set; }

        public void Start(float durationSeconds)
        {
            if (durationSeconds <= 0f)
                throw new ArgumentOutOfRangeException(nameof(durationSeconds));

            DurationSeconds = durationSeconds;
            RemainingSeconds = durationSeconds;
            IsRecharging = true;
        }

        public void Tick(float deltaTime)
        {
            if (!IsRecharging)
                return;

            RemainingSeconds -= deltaTime;

            if (RemainingSeconds < 0f)
                RemainingSeconds = 0f;
        }

        public void Stop()
        {
            IsRecharging = false;
            DurationSeconds = 0f;
            RemainingSeconds = 0f;
        }
    }
}