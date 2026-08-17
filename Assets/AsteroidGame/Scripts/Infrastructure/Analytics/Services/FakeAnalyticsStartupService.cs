using System.Threading;
using Cysharp.Threading.Tasks;
using AsteroidGame.Scripts.Infrastructure.Analytics.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Services
{
    public sealed class FakeAnalyticsStartupService : IAnalyticsStartupService
    {
        public UniTask InitializeAsync(CancellationToken cancellationToken) => UniTask.CompletedTask;
    }
}