using Zenject;
using AsteroidGame.Scripts.Gameplay.Laser.Calculations;
using AsteroidGame.Scripts.Gameplay.Laser.Contracts;
using AsteroidGame.Scripts.Gameplay.Laser.Services;
using AsteroidGame.Scripts.Gameplay.Laser.States;
using AsteroidGame.Scripts.Presentation.Laser;

namespace AsteroidGame.Scripts.Installers.Weapons
{
    public sealed class LaserBindingsInstaller : Installer<LaserBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ILaserSpawnPointProvider>()
                .To<PlayerLaserSpawnPointView>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.Bind<PlayerLaserState>().AsSingle();
            Container.Bind<PlayerLaserBeamGeometry>().AsSingle();
            Container.Bind<PlayerLaserHitArea>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerLaserShootingService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserActivationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserDamageService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserLifetimeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerLaserRechargeService>().AsSingle();
        }
    }
}