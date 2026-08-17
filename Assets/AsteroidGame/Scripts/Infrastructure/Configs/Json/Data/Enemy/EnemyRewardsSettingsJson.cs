using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy
{
    [Serializable]
    public sealed class EnemyRewardsSettingsJson
    {
        public int LargeAsteroid;
        public int MediumAsteroid;
        public int SmallAsteroid;
        public int Ufo;
    }
}