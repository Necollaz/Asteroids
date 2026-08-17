using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.World
{
    [Serializable]
    public sealed class WorldSettingsJson
    {
        public WorldSizeSettingsJson World;
        public SpawnSettingsJson Spawning;
    }
}