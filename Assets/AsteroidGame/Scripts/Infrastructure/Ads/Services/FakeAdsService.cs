using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using AsteroidGame.Scripts.Infrastructure.Ads.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Ads.Services
{
    public sealed class FakeAdsService : IAdsService
    {
        public async UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            
            Debug.Log($"{nameof(FakeAdsService)} initialized.");
        }

        public async UniTask ShowInterstitialAsync(CancellationToken cancellationToken)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            
            Debug.Log($"{nameof(FakeAdsService)} interstitial shown.");
        }

        public async UniTask<bool> ShowRewardedAsync(CancellationToken cancellationToken)
        {
            await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
            
            Debug.Log($"{nameof(FakeAdsService)} rewarded shown.");
            
            return true;
        }
    }
}