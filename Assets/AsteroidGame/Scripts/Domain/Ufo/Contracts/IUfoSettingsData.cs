namespace AsteroidGame.Scripts.Domain.Ufo.Contracts
{
    public interface IUfoSettingsData
    {
        int UfoPoolSize { get; }
        int MaxActiveUfo { get; }
        float UfoSpawnIntervalSeconds { get; }
        float UfoSpawnMargin { get; }
        float UfoSpeed { get; }
        float UfoCollisionRadius { get; }
        float UfoMaxTiltDegrees { get; }
        float UfoKnockbackSeconds { get; }
        float UfoKnockbackDamping { get; }
    }
}