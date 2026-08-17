using UnityEngine;
using AsteroidGame.Scripts.Infrastructure.Ads.Contracts;
using AsteroidGame.Scripts.Infrastructure.Core;

namespace AsteroidGame.Scripts.Infrastructure.Ads.Settings
{
    [CreateAssetMenu(
        fileName = nameof(AdsSettingsConfig),
        menuName = CoreConstants.EditorConfigsPath + nameof(AdsSettingsConfig))]
    public sealed class AdsSettingsConfig : ScriptableObject, IAdsSettingsData
    {
        [Header("Provider")]
        [SerializeField] private AdsProviderType _providerType = AdsProviderType.Fake;
        [SerializeField] private bool _initializeOnStart = true;
        [SerializeField] private bool _showInterstitialOnDefeat = true;

        [Header("Android AdMob Test Units")]
        [SerializeField] private string _androidInterstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";
        [SerializeField] private string _androidRewardedAdUnitId = "ca-app-pub-3940256099942544/5224354917";

        [Header("iOS AdMob Test Units")]
        [SerializeField] private string _iosInterstitialAdUnitId = "ca-app-pub-3940256099942544/4411468910";
        [SerializeField] private string _iosRewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313";

        public AdsProviderType ProviderType => _providerType;
        public bool InitializeOnStart => _initializeOnStart;
        public bool ShowInterstitialOnDefeat => _showInterstitialOnDefeat;

        public string InterstitialAdUnitId
        {
            get
            {
#if UNITY_IOS
                return _iosInterstitialAdUnitId;
#else
                return _androidInterstitialAdUnitId;
#endif
            }
        }

        public string RewardedAdUnitId
        {
            get
            {
#if UNITY_IOS
                return _iosRewardedAdUnitId;
#else
                return _androidRewardedAdUnitId;
#endif
            }
        }

        private void OnValidate()
        {
            if (_providerType != AdsProviderType.AdMob)
                return;

            ValidateAdUnitId(_androidInterstitialAdUnitId, nameof(_androidInterstitialAdUnitId));
            ValidateAdUnitId(_androidRewardedAdUnitId, nameof(_androidRewardedAdUnitId));
            ValidateAdUnitId(_iosInterstitialAdUnitId, nameof(_iosInterstitialAdUnitId));
            ValidateAdUnitId(_iosRewardedAdUnitId, nameof(_iosRewardedAdUnitId));
        }

        private void ValidateAdUnitId(string adUnitId, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(adUnitId))
                Debug.LogError($"{nameof(AdsSettingsConfig)} requires {fieldName}.", this);
        }
    }
}