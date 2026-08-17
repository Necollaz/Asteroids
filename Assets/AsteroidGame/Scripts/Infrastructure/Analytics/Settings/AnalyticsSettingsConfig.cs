using UnityEngine;
using AsteroidGame.Scripts.Infrastructure.Analytics.Contracts;
using AsteroidGame.Scripts.Infrastructure.Core;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Settings
{
    [CreateAssetMenu(
        fileName = nameof(AnalyticsSettingsConfig),
        menuName = CoreConstants.EditorConfigsPath + nameof(AnalyticsSettingsConfig))]
    public sealed class AnalyticsSettingsConfig : ScriptableObject, IAnalyticsSettingsData
    {
        [SerializeField] private AnalyticsProviderType _providerType = AnalyticsProviderType.Fake;
        [SerializeField] private bool _logFakeEvents = true;

        public AnalyticsProviderType ProviderType => _providerType;
        public bool LogFakeEvents => _logFakeEvents;
    }
}