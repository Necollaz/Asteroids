using System;
using System.Threading;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using AsteroidGame.Scripts.Infrastructure.Ads.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Ads.Services
{
    public sealed class AdsInitializationService : IInitializable, IDisposable
    {
        private readonly IAdsService _adsService;
        private readonly IAdsSettingsData _settings;

        private CancellationTokenSource _cancellationTokenSource;

        public AdsInitializationService(IAdsService adsService, IAdsSettingsData settings)
        {
            _adsService = adsService;
            _settings = settings;
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