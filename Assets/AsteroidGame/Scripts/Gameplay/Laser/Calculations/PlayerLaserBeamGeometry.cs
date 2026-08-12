using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Gameplay.Laser.Contracts;
using AsteroidGame.Scripts.Gameplay.Laser.Models;

namespace AsteroidGame.Scripts.Gameplay.Laser.Calculations
{
    public sealed class PlayerLaserBeamGeometry
    {
        private readonly PlayerLaserSettings _laserSettings;
        private readonly ILaserSpawnPointProvider _spawnPointProvider;

        public PlayerLaserBeamGeometry(
            PlayerLaserSettings laserSettings,
            ILaserSpawnPointProvider spawnPointProvider)
        {
            _laserSettings = laserSettings;
            _spawnPointProvider = spawnPointProvider;
        }

        public PlayerLaserBeamSegment CreateCurrentSegment()
        {
            Vector2D startPosition = _spawnPointProvider.Position;
            Vector2D direction = _spawnPointProvider.Direction;
            Vector2D endPosition = startPosition.Add(direction.Multiply(_laserSettings.Length));
            
            return new PlayerLaserBeamSegment(startPosition, endPosition);
        }
    }
}