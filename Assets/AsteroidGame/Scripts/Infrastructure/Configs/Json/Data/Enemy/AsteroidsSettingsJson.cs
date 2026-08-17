using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy
{
    [Serializable]
    public sealed class AsteroidsSettingsJson
    {
        public float SpeedReturnRate;
        public float LargeRadius;
        public float MediumRadius;
        public float SmallRadius;
        public float LargeSpeed;
        public float MediumSpeed;
        public float SmallSpeed;
        public int MediumFragmentsPerLarge;
        public int SmallFragmentsPerMedium;
    }
}