using System;
using AsteroidGame.Scripts.Domain.World;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Validation
{
    public sealed class WorldSettingsValidator
    {
        public void Validate(IWorldSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.WorldWidth <= 0f)
                throw new InvalidOperationException("World width must be greater than zero.");

            if (settings.WorldHeight <= 0f)
                throw new InvalidOperationException("World height must be greater than zero.");
        }
    }
}