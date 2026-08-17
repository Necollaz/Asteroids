using System.Threading;
using Cysharp.Threading.Tasks;

namespace AsteroidGame.Scripts.Infrastructure.Ads.Contracts
{
    public interface IAdsService
    {
        UniTask InitializeAsync(CancellationToken cancellationToken);
        UniTask ShowInterstitialAsync(CancellationToken cancellationToken);
        UniTask<bool> ShowRewardedAsync(CancellationToken cancellationToken);
    }
}