namespace AsteroidGame.Scripts.Infrastructure.Ads
{
    public interface IAdsService
    {
        void Initialize();
        
        void ShowInterstitial();
        
        void ShowRewarded();
    }
}