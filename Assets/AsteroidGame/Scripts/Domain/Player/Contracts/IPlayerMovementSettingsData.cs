namespace AsteroidGame.Scripts.Domain.Player.Contracts
{
    public interface IPlayerMovementSettingsData
    {
        float PlayerSpawnPositionX { get; }
        float PlayerSpawnPositionY { get; }
        float PlayerAcceleration { get; }
        float PlayerTurnSpeed { get; }
        float PlayerMaxSpeed { get; }
        float PlayerLinearDamping { get; }
        float PlayerSpawnRotationDegrees { get; }
    }
}