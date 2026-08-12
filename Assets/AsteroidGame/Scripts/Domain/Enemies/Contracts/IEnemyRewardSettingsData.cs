namespace AsteroidGame.Scripts.Domain.Enemies.Contracts
{
    public interface IEnemyRewardSettingsData
    {
        int LargeAsteroidReward { get; }
        int MediumAsteroidReward { get; }
        int SmallAsteroidReward { get; }
        int UfoReward { get; }
    }
}