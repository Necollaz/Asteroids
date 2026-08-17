using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Analytics;
using AsteroidGame.Scripts.Infrastructure.Analytics.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Services
{
    public sealed class FirebaseInitializationService : IFirebaseInitializationService
    {
        public bool IsInitialized { get; private set; }

        public async UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            if (IsInitialized)
                return;

            Task<DependencyStatus> dependenciesTask = FirebaseApp.CheckAndFixDependenciesAsync();

            while (!dependenciesTask.IsCompleted)
                await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);

            if (dependenciesTask.IsCanceled)
                throw new OperationCanceledException("Firebase dependency check was canceled.", cancellationToken);

            if (dependenciesTask.IsFaulted)
                throw new InvalidOperationException("Firebase dependency check failed.", dependenciesTask.Exception);

            DependencyStatus dependencyStatus = dependenciesTask.Result;

            if (dependencyStatus != DependencyStatus.Available)
            {
                throw new InvalidOperationException(
                    $"Firebase dependencies are not available. Status: {dependencyStatus}");
            }

            await UniTask.SwitchToMainThread(cancellationToken);

            try
            {
                FirebaseApp app = FirebaseApp.DefaultInstance;

                if (app == null)
                    throw new InvalidOperationException("Firebase default app is not available.");

                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                IsInitialized = true;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(
                    "Firebase app creation failed. Check Assets/google-services.json and Android Package Name.",
                    exception);
            }
        }
    }
}