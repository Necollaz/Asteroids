using Zenject;
using AsteroidGame.Scripts.Gameplay.Asteroids.Services;
using AsteroidGame.Scripts.Gameplay.Bullets.Services;
using AsteroidGame.Scripts.Gameplay.Collision;
using AsteroidGame.Scripts.Gameplay.Laser.Services;
using AsteroidGame.Scripts.Gameplay.Player.Services;
using AsteroidGame.Scripts.Gameplay.Ufo.Services;
using AsteroidGame.Scripts.Presentation.Laser;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class ExecutionOrderInstaller : Installer<ExecutionOrderInstaller>
    {
        private const int PlayerMovementExecutionOrder = -100;
        private const int EnemySimulationExecutionOrder = -90;
        private const int BulletSimulationExecutionOrder = -80;
        private const int LaserActivationExecutionOrder = -70;
        private const int LaserDamageExecutionOrder = -60;
        private const int LaserVisualExecutionOrder = -50;
        private const int LaserLifetimeExecutionOrder = -40;
        private const int CollisionSimulationExecutionOrder = -30;

        public override void InstallBindings()
        {
            Container.BindExecutionOrder<PlayerMovementService>(PlayerMovementExecutionOrder);
            Container.BindExecutionOrder<AsteroidSimulationService>(EnemySimulationExecutionOrder);
            Container.BindExecutionOrder<UfoSimulationService>(EnemySimulationExecutionOrder);
            Container.BindExecutionOrder<BulletSimulationService>(BulletSimulationExecutionOrder);
            Container.BindExecutionOrder<PlayerLaserActivationService>(LaserActivationExecutionOrder);
            Container.BindExecutionOrder<PlayerLaserDamageService>(LaserDamageExecutionOrder);
            Container.BindExecutionOrder<PlayerLaserVisualPresenter>(LaserVisualExecutionOrder);
            Container.BindExecutionOrder<PlayerLaserLifetimeService>(LaserLifetimeExecutionOrder);
            Container.BindExecutionOrder<CollisionSimulationService>(CollisionSimulationExecutionOrder);
        }
    }
}