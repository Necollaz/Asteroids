using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player
{
    [Serializable]
    public sealed class PlayerInputSettingsJson
    {
        public string InputSourceType;
        public bool ShowMobileControlsInEditor;
        public float MobileTurnLeftValue;
        public float MobileTurnRightValue;
        public string TurnLeftKey;
        public string TurnRightKey;
        public string ThrustKey;
        public string AlternativeTurnLeftKey;
        public string AlternativeTurnRightKey;
        public string AlternativeThrustKey;
        public string FireBulletKey;
        public string AlternativeFireBulletKey;
        public string FireLaserKey;
        public string AlternativeFireLaserKey;
    }
}