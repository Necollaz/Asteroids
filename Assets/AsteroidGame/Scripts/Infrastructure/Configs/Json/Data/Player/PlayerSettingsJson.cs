using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player
{
    [Serializable]
    public sealed class PlayerSettingsJson
    {
        public PlayerInputSettingsJson Input;
        public PlayerMovementSettingsJson Movement;
        public PlayerCollisionSettingsJson Collision;
        public PlayerBulletSettingsJson Bullets;
        public PlayerLaserSettingsJson Laser;
    }
}