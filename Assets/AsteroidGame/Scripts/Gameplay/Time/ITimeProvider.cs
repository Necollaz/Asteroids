namespace AsteroidGame.Scripts.Gameplay.Time
{
    public interface ITimeProvider
    {
        float DeltaTime { get; }
        float FixedDeltaTime { get; }
    }
}