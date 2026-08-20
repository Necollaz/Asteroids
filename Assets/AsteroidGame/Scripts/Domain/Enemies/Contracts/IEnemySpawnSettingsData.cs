namespace AsteroidGame.Scripts.Domain.Enemies.Contracts
{
    public interface IEnemySpawnSettingsData
    {
        int AsteroidPoolSize { get; }
        int MaxActiveAsteroids { get; }
        int UfoPoolSize { get; }
        int MaxActiveUfo { get; }
        int AsteroidExplosionPoolSize { get; }
        int UfoExplosionPoolSize { get; }
        float AsteroidSpawnIntervalSeconds { get; }
        float AsteroidSpawnMargin { get; }
        float UfoSpawnIntervalSeconds { get; }
        float UfoSpawnMargin { get; }
    }
}