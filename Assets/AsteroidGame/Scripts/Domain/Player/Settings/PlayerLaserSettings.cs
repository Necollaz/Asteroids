using System;
using AsteroidGame.Scripts.Domain.Player.Contracts;

namespace AsteroidGame.Scripts.Domain.Player.Settings
{
    public sealed class PlayerLaserSettings
    {
        public PlayerLaserSettings(IPlayerLaserSettingsData settingsData)
        {
            if (settingsData.PlayerMaxLaserCharges <= 0)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerMaxLaserCharges));
            
            if (settingsData.PlayerInitialLaserCharges < 0)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerInitialLaserCharges));
            
            if (settingsData.PlayerInitialLaserCharges > settingsData.PlayerMaxLaserCharges)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerInitialLaserCharges));
            
            MaxLaserCharges = settingsData.PlayerMaxLaserCharges;
            InitialLaserCharges = settingsData.PlayerInitialLaserCharges;
        }

        public int MaxLaserCharges { get; }
        public int InitialLaserCharges { get; }
    }
}