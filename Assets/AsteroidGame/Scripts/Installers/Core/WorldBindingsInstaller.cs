using Zenject;
using AsteroidGame.Scripts.Domain.World.Bounds;
using AsteroidGame.Scripts.Domain.World.Settings;
using AsteroidGame.Scripts.Gameplay.Enemies.Spawning;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class WorldBindingsInstaller : Installer<WorldBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<WorldSettings>().AsSingle();
            Container.Bind<WorldBounds>().AsSingle();
            Container.Bind<OutsideWorldSpawnPointSelector>().AsSingle();
        }
    }
}