using UnityEngine;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Infrastructure.Analytics.Contracts;
using AsteroidGame.Scripts.Infrastructure.Analytics.Data;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Services
{
    public sealed class FakeAnalyticsService : IAnalyticsService
    {
        private readonly IAnalyticsSettingsData _settings;

        public FakeAnalyticsService(IAnalyticsSettingsData settings) => _settings = settings;

        public void LogGameStarted() => Log(AnalyticsEventNames.GameStarted);

        public void LogPlayerDamaged(int currentHealth, int maxHealth) => 
            Log($"{AnalyticsEventNames.PlayerDamaged}" +
                $" {AnalyticsParameterNames.CurrentHealth}={currentHealth}" +
                $" {AnalyticsParameterNames.MaxHealth}={maxHealth}");

        public void LogPlayerDefeated() => Log(AnalyticsEventNames.PlayerDefeated);

        public void LogEnemyDestroyed(EnemyType enemyType) => 
            Log($"{AnalyticsEventNames.EnemyDestroyed} {AnalyticsParameterNames.EnemyType}={enemyType}");

        public void LogLaserFired(float visualWidth, float visibleSeconds) => 
            Log($"{AnalyticsEventNames.LaserFired} " +
                $"{AnalyticsParameterNames.LaserVisualWidth}={visualWidth}" +
                $" {AnalyticsParameterNames.LaserVisibleSeconds}={visibleSeconds}");

        public void LogScoreChanged(int score) => 
            Log($"{AnalyticsEventNames.ScoreChanged} {AnalyticsParameterNames.Score}={score}");

        private void Log(string message)
        {
            if (!_settings.LogFakeEvents)
                return;

            Debug.Log($"FakeAnalytics: {message}");
        }
    }
}