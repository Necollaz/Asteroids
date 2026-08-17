using System.Threading;
using Cysharp.Threading.Tasks;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Contracts
{
    public interface IAnalyticsStartupService
    {
        UniTask InitializeAsync(CancellationToken cancellationToken);
    }
}