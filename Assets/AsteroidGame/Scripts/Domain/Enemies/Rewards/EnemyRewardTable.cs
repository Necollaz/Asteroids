using System;
using System.Collections.Generic;
using AsteroidGame.Scripts.Domain.Enemies.Contracts;
using AsteroidGame.Scripts.Domain.Enemies.Types;

namespace AsteroidGame.Scripts.Domain.Enemies.Rewards
{
    public sealed class EnemyRewardTable
    {
        private readonly Dictionary<EnemyType, int> _rewards;

        public EnemyRewardTable(IEnemyRewardSettingsData settingsData)
        {
            if (settingsData == null)
                throw new ArgumentNullException(nameof(settingsData));

            _rewards = new Dictionary<EnemyType, int>
            {
                { EnemyType.LargeAsteroid, settingsData.LargeAsteroidReward },
                { EnemyType.MediumAsteroid, settingsData.MediumAsteroidReward },
                { EnemyType.SmallAsteroid, settingsData.SmallAsteroidReward },
                { EnemyType.Ufo, settingsData.UfoReward }
            };
        }

        public int GetReward(EnemyType enemyType)
        {
            if (!_rewards.TryGetValue(enemyType, out int reward))
                throw new InvalidOperationException($"Reward for enemy type {enemyType} is not configured.");

            return reward;
        }
    }
}