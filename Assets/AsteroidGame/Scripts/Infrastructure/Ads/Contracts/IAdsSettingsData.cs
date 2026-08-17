using AsteroidGame.Scripts.Infrastructure.Ads.Settings;

namespace AsteroidGame.Scripts.Infrastructure.Ads.Contracts
{
    public interface IAdsSettingsData
    {
        AdsProviderType ProviderType { get; }
        bool InitializeOnStart { get; }
        bool ShowInterstitialOnDefeat { get; }
        string InterstitialAdUnitId { get; }
        string RewardedAdUnitId { get; }
    }
}