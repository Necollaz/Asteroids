using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Services;
using AsteroidGame.Scripts.Gameplay.Collision;
using AsteroidGame.Scripts.Gameplay.Factories;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class CollisionBindingsInstaller : Installer<CollisionBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<CircleCollisionDetector>().AsSingle();
            Container.Bind<CollisionBodyRegistry>().AsSingle();
            Container.Bind<CollisionCategoryPolicy>().AsSingle();
            Container.Bind<PlayerEnemyCollisionContactResolver>().AsSingle();
            Container.Bind<PlayerEnemyCollisionHandler>().AsSingle();
            Container.Bind<CollisionContactRouter>().AsSingle();
            Container.Bind<BulletEnemyCollisionContactResolver>().AsSingle();
            Container.Bind<BulletEnemyCollisionHandler>().AsSingle();
            Container.Bind<LineCircleIntersectionDetector>().AsSingle();

            Container.BindInterfacesAndSelfTo<CollisionSimulationService>().AsSingle();

            Container.BindFactory<CollisionCategory, Body2D, float, CollisionBody, CollisionBodyFactory>();
        }
    }
}