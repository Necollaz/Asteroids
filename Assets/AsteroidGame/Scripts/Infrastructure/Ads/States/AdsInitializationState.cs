namespace AsteroidGame.Scripts.Infrastructure.Ads.States
{
    public sealed class AdsInitializationState
    {
        public bool IsInitialized { get; private set; }
        public bool HasFailed { get; private set; }

        public void MarkInitialized()
        {
            IsInitialized = true;
            HasFailed = false;
        }

        public void MarkFailed()
        {
            IsInitialized = false;
            HasFailed = true;
        }
    }
}