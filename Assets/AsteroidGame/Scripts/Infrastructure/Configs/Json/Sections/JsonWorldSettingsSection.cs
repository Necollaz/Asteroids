using System;
using AsteroidGame.Scripts.Domain.World;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.World;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections
{
    public sealed class JsonWorldSettingsSection : IWorldSettingsData
    {
        private readonly WorldSettingsJson _settings;

        public JsonWorldSettingsSection(WorldSettingsJson settings) =>
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

        public float WorldWidth => _settings.World.Width;
        public float WorldHeight => _settings.World.Height;
    }
}