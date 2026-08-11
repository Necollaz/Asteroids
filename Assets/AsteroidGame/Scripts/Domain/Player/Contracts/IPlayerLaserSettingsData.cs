namespace AsteroidGame.Scripts.Domain.Player.Contracts
{
    public interface IPlayerLaserSettingsData
    {
        int PlayerMaxLaserCharges { get; }
        int PlayerInitialLaserCharges { get; }
    }
}