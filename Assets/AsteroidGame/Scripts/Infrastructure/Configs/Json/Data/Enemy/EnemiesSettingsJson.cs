using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy
{
    [Serializable]
    public sealed class EnemiesSettingsJson
    {
        public AsteroidsSettingsJson Asteroids;
        public UfoSettingsJson Ufo;
        public EnemyRewardsSettingsJson Rewards;
    }
}