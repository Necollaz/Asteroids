using Zenject;
using AsteroidGame.Scripts.Presentation.Laser;
using AsteroidGame.Scripts.Presentation.Player.Presenters;
using AsteroidGame.Scripts.Presentation.Player.Views;

namespace AsteroidGame.Scripts.Installers.Presentation
{
    public sealed class GameplayPresentationInstaller : Installer<GameplayPresentationInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerViewPresenter>().AsSingle();

            Container.Bind<PlayerInvulnerabilityEffectView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInvulnerabilityPresenter>().AsSingle();

            Container.Bind<PlayerLaserView>().FromComponentInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserVisualPresenter>().AsSingle();
        }
    }
}