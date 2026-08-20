namespace AsteroidGame.Scripts.Domain.Asteroids.Contracts
{
    public interface IAsteroidSettingsData
    {
        float AsteroidSpeedReturnRate { get; }
        float LargeAsteroidRadius { get; }
        float MediumAsteroidRadius { get; }
        float SmallAsteroidRadius { get; }
        float LargeAsteroidSpeed { get; }
        float MediumAsteroidSpeed { get; }
        float SmallAsteroidSpeed { get; }
        int MediumFragmentsPerLarge { get; }
        int SmallFragmentsPerMedium { get; }
    }
}