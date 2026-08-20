using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using AsteroidGame.Scripts.Infrastructure.Ads.Contracts;

#if GOOGLE_MOBILE_ADS
using GoogleMobileAds.Api;
#endif

namespace AsteroidGame.Scripts.Infrastructure.Ads.Services
{
    public sealed class AdMobAdsService : IAdsService, IDisposable
    {
        private readonly IAdsSettingsData _settings;

        private bool _isInitialized;

#if GOOGLE_MOBILE_ADS
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;
#endif

        public AdMobAdsService(IAdsSettingsData settings) => _settings = settings;

        public async UniTask InitializeAsync(CancellationToken cancellationToken)
        {
#if GOOGLE_MOBILE_ADS
            if (_isInitialized)
                return;

            UniTaskCompletionSource completionSource = new UniTaskCompletionSource();
            MobileAds.Initialize(_ => completionSource.TrySetResult());

            await completionSource.Task.AttachExternalCancellation(cancellationToken);

            _isInitialized = true;

            await LoadInterstitialAsync(cancellationToken);
            await LoadRewardedAsync(cancellationToken);
#else
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);

            throw new InvalidOperationException(
                $"{nameof(AdMobAdsService)} requires Google Mobile Ads Unity plugin and GOOGLE_MOBILE_ADS define.");
#endif
        }

        public async UniTask ShowInterstitialAsync(CancellationToken cancellationToken)
        {
#if GOOGLE_MOBILE_ADS
            EnsureInitialized();

            if (_interstitialAd == null || !_interstitialAd.CanShowAd())
                await LoadInterstitialAsync(cancellationToken);

            if (_interstitialAd == null || !_interstitialAd.CanShowAd())
                return;

            UniTaskCompletionSource completionSource = new UniTaskCompletionSource();

            void HandleClosed()
            {
                completionSource.TrySetResult();
            }

            void HandleFailed(AdError error)
            {
                completionSource.TrySetException(
                    new InvalidOperationException($"AdMob interstitial failed to show: {error}"));
            }

            _interstitialAd.OnAdFullScreenContentClosed += HandleClosed;
            _interstitialAd.OnAdFullScreenContentFailed += HandleFailed;

            try
            {
                _interstitialAd.Show();

                await completionSource.Task.AttachExternalCancellation(cancellationToken);
            }
            finally
            {
                if (_interstitialAd != null)
                {
                    _interstitialAd.OnAdFullScreenContentClosed -= HandleClosed;
                    _interstitialAd.OnAdFullScreenContentFailed -= HandleFailed;
                }
            }

            DestroyInterstitial();
            await LoadInterstitialAsync(cancellationToken);
#else
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);

            throw new InvalidOperationException(
                $"{nameof(AdMobAdsService)} requires Google Mobile Ads Unity plugin and GOOGLE_MOBILE_ADS define.");
#endif
        }

        public async UniTask<bool> ShowRewardedAsync(CancellationToken cancellationToken)
        {
#if GOOGLE_MOBILE_ADS
            EnsureInitialized();

            if (_rewardedAd == null || !_rewardedAd.CanShowAd())
                await LoadRewardedAsync(cancellationToken);

            if (_rewardedAd == null || !_rewardedAd.CanShowAd())
                return false;

            bool rewardEarned = false;
            UniTaskCompletionSource<bool> completionSource = new UniTaskCompletionSource<bool>();

            void HandleClosed()
            {
                completionSource.TrySetResult(rewardEarned);
            }

            void HandleFailed(AdError error)
            {
                completionSource.TrySetException(
                    new InvalidOperationException($"AdMob rewarded failed to show: {error}"));
            }

            _rewardedAd.OnAdFullScreenContentClosed += HandleClosed;
            _rewardedAd.OnAdFullScreenContentFailed += HandleFailed;

            bool result;

            try
            {
                _rewardedAd.Show(_ => rewardEarned = true);

                result = await completionSource.Task.AttachExternalCancellation(cancellationToken);
            }
            finally
            {
                if (_rewardedAd != null)
                {
                    _rewardedAd.OnAdFullScreenContentClosed -= HandleClosed;
                    _rewardedAd.OnAdFullScreenContentFailed -= HandleFailed;
                }
            }

            DestroyRewarded();

            await LoadRewardedAsync(cancellationToken);

            return result;
#else
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);

            throw new InvalidOperationException(
                $"{nameof(AdMobAdsService)} requires Google Mobile Ads Unity plugin and GOOGLE_MOBILE_ADS define.");
#endif
        }

        public void Dispose()
        {
#if GOOGLE_MOBILE_ADS
            DestroyInterstitial();
            DestroyRewarded();
#endif
        }

#if GOOGLE_MOBILE_ADS
        private async UniTask LoadInterstitialAsync(CancellationToken cancellationToken)
        {
            DestroyInterstitial();

            UniTaskCompletionSource completionSource = new UniTaskCompletionSource();
            AdRequest request = new AdRequest();

            InterstitialAd.Load(_settings.InterstitialAdUnitId, request, (ad, error) =>
            {
                if (error != null)
                {
                    completionSource.TrySetException(
                        new InvalidOperationException($"AdMob interstitial failed to load: {error}"));

                    return;
                }

                _interstitialAd = ad;
                completionSource.TrySetResult();
            });

            await completionSource.Task.AttachExternalCancellation(cancellationToken);
        }

        private async UniTask LoadRewardedAsync(CancellationToken cancellationToken)
        {
            DestroyRewarded();

            UniTaskCompletionSource completionSource = new UniTaskCompletionSource();
            AdRequest request = new AdRequest();

            RewardedAd.Load(_settings.RewardedAdUnitId, request, (ad, error) =>
            {
                if (error != null)
                {
                    completionSource.TrySetException(
                        new InvalidOperationException($"AdMob rewarded failed to load: {error}"));

                    return;
                }

                _rewardedAd = ad;
                completionSource.TrySetResult();
            });

            await completionSource.Task.AttachExternalCancellation(cancellationToken);
        }

        private void DestroyInterstitial()
        {
            if (_interstitialAd == null)
                return;

            _interstitialAd.Destroy();
            _interstitialAd = null;
        }

        private void DestroyRewarded()
        {
            if (_rewardedAd == null)
                return;

            _rewardedAd.Destroy();
            _rewardedAd = null;
        }
#endif

        private void EnsureInitialized()
        {
            if (!_isInitialized)
                throw new InvalidOperationException($"{nameof(AdMobAdsService)} is not initialized.");
        }
    }
}