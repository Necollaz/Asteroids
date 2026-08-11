namespace AsteroidGame.Scripts.Domain.Player.Contracts
{
    public interface IPlayerLaserSettingsData
    {
        int PlayerMaxLaserCharges { get; }
        int PlayerInitialLaserCharges { get; }
        float PlayerLaserRechargeSeconds { get; }
        float PlayerLaserVisibleSeconds { get; }
        float PlayerLaserLength { get; }
        float PlayerLaserHitHalfWidth { get; }
        float PlayerLaserVisualWidth { get; }
    }
}