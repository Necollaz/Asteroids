using Firebase.Analytics;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Infrastructure.Analytics.Contracts;
using AsteroidGame.Scripts.Infrastructure.Analytics.Data;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Services
{
    public sealed class FirebaseAnalyticsService : IAnalyticsService
    {
        private readonly IFirebaseInitializationService _firebaseInitializationService;

         public FirebaseAnalyticsService(IFirebaseInitializationService firebaseInitializationService) =>
            _firebaseInitializationService = firebaseInitializationService;

        public void LogGameStarted()
        {
            if (!_firebaseInitializationService.IsInitialized)
                return;

            FirebaseAnalytics.LogEvent(AnalyticsEventNames.GameStarted);
        }

        public void LogPlayerDamaged(int currentHealth, int maxHealth)
        {
            if (!_firebaseInitializationService.IsInitialized)
                return;

            FirebaseAnalytics.LogEvent(
                AnalyticsEventNames.PlayerDamaged,
                new Parameter(AnalyticsParameterNames.CurrentHealth, currentHealth),
                new Parameter(AnalyticsParameterNames.MaxHealth, maxHealth));
        }

        public void LogPlayerDefeated()
        {
            if (!_firebaseInitializationService.IsInitialized)
                return;

            FirebaseAnalytics.LogEvent(AnalyticsEventNames.PlayerDefeated);
        }

        public void LogEnemyDestroyed(EnemyType enemyType)
        {
            if (!_firebaseInitializationService.IsInitialized)
                return;

            FirebaseAnalytics.LogEvent(
                AnalyticsEventNames.EnemyDestroyed,
                new Parameter(AnalyticsParameterNames.EnemyType, enemyType.ToString()));
        }

        public void LogLaserFired(float visualWidth, float visibleSeconds)
        {
            if (!_firebaseInitializationService.IsInitialized)
                return;

            FirebaseAnalytics.LogEvent(
                AnalyticsEventNames.LaserFired,
                new Parameter(AnalyticsParameterNames.LaserVisualWidth, (double)visualWidth),
                new Parameter(AnalyticsParameterNames.LaserVisibleSeconds, (double)visibleSeconds));
        }

        public void LogScoreChanged(int score)
        {
            if (!_firebaseInitializationService.IsInitialized)
                return;

            FirebaseAnalytics.LogEvent(
                AnalyticsEventNames.ScoreChanged,
                new Parameter(AnalyticsParameterNames.Score, score));
        }
    }
}