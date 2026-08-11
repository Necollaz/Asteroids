using System;
using AsteroidGame.Scripts.Domain.Player.Contracts;

namespace AsteroidGame.Scripts.Domain.Player.Settings
{
    public sealed class PlayerLaserSettings
    {
        public PlayerLaserSettings(IPlayerLaserSettingsData settingsData)
        {
            if (settingsData == null)
                throw new ArgumentNullException(nameof(settingsData));

            if (settingsData.PlayerMaxLaserCharges <= 0)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerMaxLaserCharges));

            if (settingsData.PlayerInitialLaserCharges < 0)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerInitialLaserCharges));

            if (settingsData.PlayerInitialLaserCharges > settingsData.PlayerMaxLaserCharges)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerInitialLaserCharges));

            if (settingsData.PlayerLaserRechargeSeconds <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerLaserRechargeSeconds));

            if (settingsData.PlayerLaserVisibleSeconds <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerLaserVisibleSeconds));

            if (settingsData.PlayerLaserLength <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerLaserLength));

            if (settingsData.PlayerLaserHitHalfWidth < 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerLaserHitHalfWidth));

            if (settingsData.PlayerLaserVisualWidth <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerLaserVisualWidth));

            MaxLaserCharges = settingsData.PlayerMaxLaserCharges;
            InitialLaserCharges = settingsData.PlayerInitialLaserCharges;
            RechargeSeconds = settingsData.PlayerLaserRechargeSeconds;
            VisibleSeconds = settingsData.PlayerLaserVisibleSeconds;
            Length = settingsData.PlayerLaserLength;
            HitHalfWidth = settingsData.PlayerLaserHitHalfWidth;
            VisualWidth = settingsData.PlayerLaserVisualWidth;
        }

        public int MaxLaserCharges { get; }
        public int InitialLaserCharges { get; }
        public float RechargeSeconds { get; }
        public float VisibleSeconds { get; }
        public float Length { get; }
        public float HitHalfWidth { get; }
        public float VisualWidth { get; }
    }
}