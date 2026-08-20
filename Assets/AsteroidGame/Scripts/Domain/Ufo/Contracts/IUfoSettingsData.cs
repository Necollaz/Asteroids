namespace AsteroidGame.Scripts.Domain.Ufo.Contracts
{
    public interface IUfoSettingsData
    {
        float UfoSpeed { get; }
        float UfoCollisionRadius { get; }
        float UfoMaxTiltDegrees { get; }
        float UfoKnockbackSeconds { get; }
        float UfoKnockbackDamping { get; }
    }
}