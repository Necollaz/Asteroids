using AsteroidGame.Scripts.Infrastructure.Analytics.Settings;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Contracts
{
    public interface IAnalyticsSettingsData
    {
        AnalyticsProviderType ProviderType { get; }
        bool LogFakeEvents { get; }
    }
}