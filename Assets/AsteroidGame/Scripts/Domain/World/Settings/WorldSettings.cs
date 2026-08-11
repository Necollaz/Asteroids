using System;

namespace AsteroidGame.Scripts.Domain.World.Settings
{
    public sealed class WorldSettings
    {
        public WorldSettings(IWorldSettingsData settingsData)
        {
            if (settingsData.WorldWidth <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.WorldWidth));

            if (settingsData.WorldHeight <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.WorldHeight));

            Width = settingsData.WorldWidth;
            Height = settingsData.WorldHeight;
        }

        public float Width { get; }
        public float Height { get; }
    }
}