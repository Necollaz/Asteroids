using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Enemies.Rewards;
using AsteroidGame.Scripts.Domain.Score;
using AsteroidGame.Scripts.Signals.Enemies;
using AsteroidGame.Scripts.Signals.Score;

namespace AsteroidGame.Scripts.Gameplay.Score
{
    public sealed class EnemyRewardService : IInitializable, IDisposable
    {
        private readonly EnemyRewardTable _rewardTable;
        private readonly ScoreState _scoreState;
        private readonly SignalBus _signalBus;

        public EnemyRewardService(
            EnemyRewardTable rewardTable,
            ScoreState scoreState,
            SignalBus signalBus)
        {
            _rewardTable = rewardTable;
            _scoreState = scoreState;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize() => _signalBus.Subscribe<EnemyDestroyedSignal>(HandleEnemyDestroyed);
        
        void IDisposable.Dispose() => _signalBus.Unsubscribe<EnemyDestroyedSignal>(HandleEnemyDestroyed);

        private void HandleEnemyDestroyed(EnemyDestroyedSignal signal)
        {
            int reward = _rewardTable.GetReward(signal.EnemyType);
            _scoreState.Add(reward);
            _signalBus.Fire(new ScoreChangedSignal(_scoreState.Value));
        }
    }
}