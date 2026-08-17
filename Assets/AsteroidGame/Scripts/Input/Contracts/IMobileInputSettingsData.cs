namespace AsteroidGame.Scripts.Input.Contracts
{
    public interface IMobileInputSettingsData
    {
        bool ShowMobileControlsInEditor { get; }
        float MobileTurnLeftValue { get; }
        float MobileTurnRightValue { get; }
    }
}