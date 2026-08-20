using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Factories;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Gameplay.Factories;
using AsteroidGame.Scripts.Gameplay.Player.Calculations;
using AsteroidGame.Scripts.Gameplay.Player.Facades;
using AsteroidGame.Scripts.Gameplay.Player.Factories;
using AsteroidGame.Scripts.Gameplay.Player.Services;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.Installers.Player
{
    public sealed class PlayerBindingsInstaller : Installer<PlayerBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PlayerMovementSettings>().AsSingle();
            Container.Bind<PlayerCollisionSettings>().AsSingle();
            Container.Bind<PlayerLaserSettings>().AsSingle();
            Container.Bind<PlayerHealthState>().AsSingle();
            Container.Bind<PlayerInvulnerabilityState>().AsSingle();
            Container.Bind<PlayerLaserMagazine>().AsSingle();
            Container.Bind<PlayerLaserRechargeState>().AsSingle();
            Container.Bind<PlayerSnapshotFactory>().AsSingle();
            Container.Bind<ITimeProvider>().To<UnityTimeProvider>().AsSingle();
            Container.Bind<PlayerAccelerationCalculator>().AsSingle();

            Container.BindFactory<Body2D, PlayerModel, PlayerModelFactory>();
            
            Container.Bind<PlayerModelCreationFactory>().AsSingle();
            Container.Bind<PlayerModel>()
                .FromResolveGetter<PlayerModelCreationFactory>(factory => factory.Create())
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerFacade>().AsSingle();

            Container.Bind<PlayerDamageService>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerMovementService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerCollisionBodyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInvulnerabilityTimerService>().AsSingle();
        }
    }
}