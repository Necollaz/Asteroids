namespace AsteroidGame.Scripts.Domain.Player
{
    public interface IPlayerCollisionSettingsData
    {
        int PlayerMaxHealth { get; }
        float PlayerCollisionRadius { get; }
        float PlayerCollisionBounceSpeed { get; }
        float PlayerInvulnerabilitySeconds { get; }
    }
}