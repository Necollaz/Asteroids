using System;
using System.Threading;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using AsteroidGame.Scripts.Infrastructure.Analytics.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Presenters
{
    public sealed class AnalyticsInitializationService : IInitializable, IDisposable
    {
        private readonly IAnalyticsStartupService _analyticsStartupService;
        private readonly IAnalyticsService _analyticsService;

        private CancellationTokenSource _cancellationTokenSource;

        public AnalyticsInitializationService(
            IAnalyticsStartupService analyticsStartupService,
            IAnalyticsService analyticsService)
        {
            _analyticsStartupService = analyticsStartupService;
            _analyticsService = analyticsService;
        }

        void IInitializable.Initialize()
        {
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
                await _analyticsStartupService.InitializeAsync(cancellationToken);
                _analyticsService.LogGameStarted();
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