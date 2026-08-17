using System.Threading;
using Cysharp.Threading.Tasks;

namespace AsteroidGame.Scripts.Infrastructure.Analytics.Contracts
{
    public interface IFirebaseInitializationService
    {
        bool IsInitialized { get; }

        UniTask InitializeAsync(CancellationToken cancellationToken);
    }
}