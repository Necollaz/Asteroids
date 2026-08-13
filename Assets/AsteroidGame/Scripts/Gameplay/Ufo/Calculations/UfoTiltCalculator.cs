using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Ufo.Settings;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Calculations
{
    public sealed class UfoTiltCalculator
    {
        private const float DirectionDeadZone = 0.001f;
        
        private readonly UfoSettings _settings;
        
        public UfoTiltCalculator(UfoSettings settings) => _settings = settings;

        public float Calculate(Vector2D movementDirection)
        {
            if (movementDirection.X > DirectionDeadZone)
                return -_settings.MaxTiltDegrees;

            if (movementDirection.X < -DirectionDeadZone)
                return _settings.MaxTiltDegrees;

            return 0f;
        }
    }
}