namespace AsteroidGame.Scripts.Domain.Bullets.Contracts
{
    public interface IBulletSettingsData
    {
        float BulletSpeed { get; }
        float BulletLifetimeSeconds { get; }
        float BulletRadius { get; }
        float BulletShotsPerSecond { get; }
        float BulletSpawnOffset { get; }
        float BulletVisibilityMargin { get; }
        int PoolSize { get; }
    }
}