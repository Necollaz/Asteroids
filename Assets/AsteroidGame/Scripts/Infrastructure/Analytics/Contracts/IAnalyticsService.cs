using AsteroidGame.Scripts.Domain.Enemies.Types;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Contracts
{
    public interface IAnalyticsService
    {
        void LogGameStarted();
        void LogPlayerDamaged(int currentHealth, int maxHealth);
        void LogPlayerDefeated();
        void LogEnemyDestroyed(EnemyType enemyType);
        void LogLaserFired(float visualWidth, float visibleSeconds);
        void LogScoreChanged(int score);
    }
}