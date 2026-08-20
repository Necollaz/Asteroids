using Zenject;
using AsteroidGame.Scripts.Domain.Enemies.Mapping;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Domain.Enemies.Settings;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Enemies.Facades;
using AsteroidGame.Scripts.Gameplay.Enemies.Factories;

namespace AsteroidGame.Scripts.Installers.Enemies
{
    public sealed class EnemyRegistryBindingsInstaller : Installer<EnemyRegistryBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyFacade>().AsSingle();
            Container.Bind<EnemySpawnSettings>().AsSingle();
            Container.Bind<EnemyCollisionCategoryMapper>().AsSingle();
            Container.Bind<EnemyInstanceContextFactory>().AsSingle();

            Container.BindFactory<EnemyType, Body2D, EnemyModel, EnemyModelFactory>();
        }
    }
}