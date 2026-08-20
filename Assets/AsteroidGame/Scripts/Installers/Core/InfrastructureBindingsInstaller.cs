using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Random;
using AsteroidGame.Scripts.Infrastructure.Ads.Contracts;
using AsteroidGame.Scripts.Infrastructure.Ads.Presenters;
using AsteroidGame.Scripts.Infrastructure.Ads.Services;
using AsteroidGame.Scripts.Infrastructure.Ads.Settings;
using AsteroidGame.Scripts.Infrastructure.Ads.States;
using AsteroidGame.Scripts.Infrastructure.Analytics.Contracts;
using AsteroidGame.Scripts.Infrastructure.Analytics.Presenters;
using AsteroidGame.Scripts.Infrastructure.Analytics.Services;
using AsteroidGame.Scripts.Infrastructure.Analytics.Settings;
using AsteroidGame.Scripts.Infrastructure.Random;
using AsteroidGame.Scripts.Infrastructure.Scenes;
using AsteroidGame.Scripts.Installers.SceneReferences;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class InfrastructureBindingsInstaller :
        Installer<IGameplaySceneReferences, InfrastructureBindingsInstaller>
    {
        private readonly IGameplaySceneReferences _sceneReferences;

        public InfrastructureBindingsInstaller(IGameplaySceneReferences sceneReferences) => 
            _sceneReferences = sceneReferences;

        public override void InstallBindings()
        {
            Container.Bind<IRandomValueProvider>().To<UnityRandomValueProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneRestartService>().AsSingle();

            BindAds();
            BindAnalytics();
        }

        private void BindAds()
        {
            AdsSettingsConfig adsSettingsConfig = _sceneReferences.AdsSettingsConfig;

            if (adsSettingsConfig == null)
                throw new InvalidOperationException("AdsSettingsConfig is not assigned in GameInstaller.");

            Container.Bind<IAdsSettingsData>().FromInstance(adsSettingsConfig).AsSingle();
            Container.Bind<AdsInitializationState>().AsSingle();
            
            if (adsSettingsConfig.ProviderType == AdsProviderType.AdMob)
                Container.Bind<IAdsService>().To<AdMobAdsService>().AsSingle();
            else
                Container.Bind<IAdsService>().To<FakeAdsService>().AsSingle();

            Container.BindInterfacesAndSelfTo<AdsInitializationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<DefeatInterstitialAdsPresenter>().AsSingle();
        }

        private void BindAnalytics()
        {
            AnalyticsSettingsConfig analyticsSettingsConfig = _sceneReferences.AnalyticsSettingsConfig;

            if (analyticsSettingsConfig == null)
                throw new InvalidOperationException("AnalyticsSettingsConfig is not assigned in GameInstaller.");

            Container.Bind<IAnalyticsSettingsData>().FromInstance(analyticsSettingsConfig).AsSingle();

            if (analyticsSettingsConfig.ProviderType == AnalyticsProviderType.Firebase)
                BindFirebaseAnalytics();
            else
                BindFakeAnalytics();

            Container.BindInterfacesAndSelfTo<AnalyticsInitializationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsSignalPresenter>().AsSingle();
        }

        private void BindFirebaseAnalytics()
        {
            Container.BindInterfacesAndSelfTo<FirebaseInitializationService>().AsSingle();
            Container.Bind<IAnalyticsService>().To<FirebaseAnalyticsService>().AsSingle();
        }

        private void BindFakeAnalytics()
        {
            Container.Bind<IAnalyticsStartupService>().To<FakeAnalyticsStartupService>().AsSingle();
            Container.Bind<IAnalyticsService>().To<FakeAnalyticsService>().AsSingle();
        }
    }
}