using Zenject;
using AsteroidGame.Scripts.Gameplay.Enemies.Facades;

namespace AsteroidGame.Scripts.Installers.Enemies
{
    public sealed class EnemyRegistryBindingsInstaller : Installer<EnemyRegistryBindingsInstaller>
    {
        public override void InstallBindings() => Container.Bind<EnemyFacade>().AsSingle();
    }
}