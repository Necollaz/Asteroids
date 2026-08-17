using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player
{
    [Serializable]
    public sealed class PlayerLaserSettingsJson
    {
        public int MaxCharges;
        public int InitialCharges;
        public float RechargeSeconds;
        public float VisibleSeconds;
        public float Length;
        public float HitHalfWidth;
        public float VisualWidth;
    }
}