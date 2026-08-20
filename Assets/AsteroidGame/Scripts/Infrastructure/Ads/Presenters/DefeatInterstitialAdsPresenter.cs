using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using AsteroidGame.Scripts.Infrastructure.Ads.Contracts;
using AsteroidGame.Scripts.Infrastructure.Ads.States;
using AsteroidGame.Scripts.Signals.Game;

namespace AsteroidGame.Scripts.Infrastructure.Ads.Presenters
{
    public sealed class DefeatInterstitialAdsPresenter : IInitializable, IDisposable
    {
        private const string AdsNotInitializedMessage = "Interstitial ad skipped. Ads service is not initialized.";
        
        private readonly IAdsService _adsService;
        private readonly IAdsSettingsData _settings;
        private readonly AdsInitializationState _initializationState;
        private readonly SignalBus _signalBus;
        
        private CancellationTokenSource _cancellationTokenSource;

        public DefeatInterstitialAdsPresenter(
            IAdsService adsService,
            IAdsSettingsData settings,
            AdsInitializationState initializationState,
            SignalBus signalBus)
        {
            _adsService = adsService;
            _settings = settings;
            _initializationState = initializationState;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _signalBus.Subscribe<GameDefeatStartedSignal>(ShowInterstitial);
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<GameDefeatStartedSignal>(ShowInterstitial);
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }

        private void ShowInterstitial()
        {
            if (!_settings.ShowInterstitialOnDefeat)
                return;

            if (!_initializationState.IsInitialized)
            {
                Debug.LogWarning(AdsNotInitializedMessage);
                
                return;
            }
            
            ShowInterstitialAsync(_cancellationTokenSource.Token).Forget();
        }

        private async UniTask ShowInterstitialAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _adsService.ShowInterstitialAsync(cancellationToken);
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception)
            {
                Debug.LogException(exception);
            }
        }
    }
}