using Zenject;
using AsteroidGame.Scripts.Domain.Enemies.Rewards;
using AsteroidGame.Scripts.Domain.Score;
using AsteroidGame.Scripts.Gameplay.Score;

namespace AsteroidGame.Scripts.Installers.Enemies
{
    public sealed class ScoreBindingsInstaller : Installer<ScoreBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ScoreState>().AsSingle();
            Container.Bind<EnemyRewardTable>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyRewardService>().AsSingle();
        }
    }
}