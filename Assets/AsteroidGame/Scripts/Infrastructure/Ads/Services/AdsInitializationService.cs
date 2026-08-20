using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using AsteroidGame.Scripts.Infrastructure.Ads.Contracts;
using AsteroidGame.Scripts.Infrastructure.Ads.States;

namespace AsteroidGame.Scripts.Infrastructure.Ads.Services
{
    public sealed class AdsInitializationService : IInitializable, IDisposable
    {
        private readonly IAdsService _adsService;
        private readonly IAdsSettingsData _settings;
        private readonly AdsInitializationState _initializationState;

        private CancellationTokenSource _cancellationTokenSource;

        public AdsInitializationService(
            IAdsService adsService,
            IAdsSettingsData settings,
            AdsInitializationState initializationState)
        {
            _adsService = adsService;
            _settings = settings;
            _initializationState = initializationState;
        }

        void IInitializable.Initialize()
        {
            if (!_settings.InitializeOnStart)
                return;

            _cancellationTokenSource = new CancellationTokenSource();
            InitializeAsync(_cancellationTokenSource.Token).Forget();
        }

        void IDisposable.Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private async UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _adsService.InitializeAsync(cancellationToken);
                _initializationState.MarkInitialized();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception exception)
            {
                _initializationState.MarkFailed();
                Debug.LogException(exception);
            }
        }
    }
}