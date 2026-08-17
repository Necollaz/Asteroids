namespace AsteroidGame.Scripts.Input.Contracts
{
    public interface IPlayerInputPlatform
    {
        bool IsEditor { get; }
        bool IsMobilePlatform { get; }
    }
}