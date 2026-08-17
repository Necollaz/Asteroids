using Zenject;
using AsteroidGame.Scripts.UI.Common;
using AsteroidGame.Scripts.UI.Game;
using AsteroidGame.Scripts.UI.Player;
using AsteroidGame.Scripts.UI.Player.Stats;

namespace AsteroidGame.Scripts.Installers.Presentation
{
    public sealed class UiBindingsInstaller : Installer<UiBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<UiEventSystemView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<UiEventSystemPresenter>().AsSingle();

            Container.Bind<PlayerHealthView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerHealthHudSettings>().AsSingle();
            Container.Bind<PlayerHealthViewModelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerHealthPresenter>().AsSingle();

            Container.Bind<DefeatGameView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<DefeatGamePresenter>().AsSingle();

            Container.Bind<PlayerStatsHudView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<PlayerStatsHudViewModelFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStatsHudPresenter>().AsSingle();
        }
    }
}