namespace AsteroidGame.Scripts.Domain.Player.Contracts
{
    public interface IPlayerCollisionSettingsData
    {
        int PlayerMaxHealth { get; }
        float PlayerCollisionRadius { get; }
        float PlayerCollisionBounceSpeed { get; }
        float PlayerInvulnerabilitySeconds { get; }
    }
}