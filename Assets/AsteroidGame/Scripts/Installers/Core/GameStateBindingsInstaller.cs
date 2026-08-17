using Zenject;
using AsteroidGame.Scripts.Gameplay.Game;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class GameStateBindingsInstaller : Installer<GameStateBindingsInstaller>
    {
        public override void InstallBindings() => Container.BindInterfacesAndSelfTo<GameStateService>().AsSingle();
    }
}