using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Calculations;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Physics.Services;
using AsteroidGame.Scripts.Gameplay.Factories;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class PhysicsBindingsInstaller : Installer<PhysicsBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<PhysicsValueFactory>().AsSingle();
            Container.Bind<Direction2DCalculator>().AsSingle();
            Container.Bind<CustomPhysicsWorld>().AsSingle();

            Container.BindFactory<Vector2D, Velocity, float, Body2D, Body2DFactory>();
        }
    }
}