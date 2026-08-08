namespace AsteroidGame.Scripts.Infrastructure.Analytics
{
    public interface IAnalyticsService
    {
        void LogGameStarted();
        
        void LogGameOver(int score);
        
        void LogEnemyDestroyed(string enemyType);
        
        void LogLaserUsed();
    }
}