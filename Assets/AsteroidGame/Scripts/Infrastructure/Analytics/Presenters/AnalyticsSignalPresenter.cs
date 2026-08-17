using System;
using Zenject;
using AsteroidGame.Scripts.Infrastructure.Analytics.Contracts;
using AsteroidGame.Scripts.Signals.Enemies;
using AsteroidGame.Scripts.Signals.Player;
using AsteroidGame.Scripts.Signals.Score;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Presenters
{
    public sealed class AnalyticsSignalPresenter : IInitializable, IDisposable
    {
        private readonly IAnalyticsService _analyticsService;
        private readonly SignalBus _signalBus;

        public AnalyticsSignalPresenter(IAnalyticsService analyticsService, SignalBus signalBus)
        {
            _analyticsService = analyticsService;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<PlayerDamagedSignal>(HandlePlayerDamaged);
            _signalBus.Subscribe<PlayerDefeatedSignal>(HandlePlayerDefeated);
            _signalBus.Subscribe<EnemyDestroyedSignal>(HandleEnemyDestroyed);
            _signalBus.Subscribe<PlayerLaserFiredSignal>(HandleLaserFired);
            _signalBus.Subscribe<ScoreChangedSignal>(HandleScoreChanged);
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<PlayerDamagedSignal>(HandlePlayerDamaged);
            _signalBus.Unsubscribe<PlayerDefeatedSignal>(HandlePlayerDefeated);
            _signalBus.Unsubscribe<EnemyDestroyedSignal>(HandleEnemyDestroyed);
            _signalBus.Unsubscribe<PlayerLaserFiredSignal>(HandleLaserFired);
            _signalBus.Unsubscribe<ScoreChangedSignal>(HandleScoreChanged);
        }

        private void HandlePlayerDamaged(PlayerDamagedSignal signal) => 
            _analyticsService.LogPlayerDamaged(signal.CurrentHealth, signal.MaxHealth);

        private void HandlePlayerDefeated() => _analyticsService.LogPlayerDefeated();

        private void HandleEnemyDestroyed(EnemyDestroyedSignal signal) => 
            _analyticsService.LogEnemyDestroyed(signal.EnemyType);

        private void HandleLaserFired(PlayerLaserFiredSignal signal) => 
            _analyticsService.LogLaserFired(signal.VisualWidth, signal.VisibleSeconds);

        private void HandleScoreChanged(ScoreChangedSignal signal) => _analyticsService.LogScoreChanged(signal.Score);
    }
}