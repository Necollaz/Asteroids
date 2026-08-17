using System;
using System.Threading;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using AsteroidGame.Scripts.Infrastructure.Ads.Contracts;
using AsteroidGame.Scripts.Signals.Game;

namespace AsteroidGame.Scripts.Infrastructure.Ads.Presenters
{
    public sealed class DefeatInterstitialAdsPresenter : IInitializable, IDisposable
    {
        private readonly IAdsService _adsService;
        private readonly IAdsSettingsData _settings;
        private readonly SignalBus _signalBus;
        
        private CancellationTokenSource _cancellationTokenSource;

        public DefeatInterstitialAdsPresenter(IAdsService adsService, IAdsSettingsData settings, SignalBus signalBus)
        {
            _adsService = adsService;
            _settings = settings;
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