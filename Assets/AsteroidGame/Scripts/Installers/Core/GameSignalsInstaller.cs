using Zenject;
using AsteroidGame.Scripts.Signals.Enemies;
using AsteroidGame.Scripts.Signals.Game;
using AsteroidGame.Scripts.Signals.Player;
using AsteroidGame.Scripts.Signals.Score;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class GameSignalsInstaller : Installer<GameSignalsInstaller>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PlayerDamagedSignal>();
            Container.DeclareSignal<PlayerDefeatedSignal>();
            Container.DeclareSignal<PlayerInvulnerabilityStartedSignal>();
            Container.DeclareSignal<PlayerInvulnerabilityEndedSignal>();
            Container.DeclareSignal<GameDefeatStartedSignal>();
            Container.DeclareSignal<GameRestartRequestedSignal>();
            Container.DeclareSignal<EnemyHitByBulletSignal>().OptionalSubscriber();
            Container.DeclareSignal<PlayerLaserFiredSignal>();
            Container.DeclareSignal<PlayerLaserChargesChangedSignal>().OptionalSubscriber();
            Container.DeclareSignal<EnemyHitByLaserSignal>().OptionalSubscriber();
            Container.DeclareSignal<EnemyDestroyedSignal>().OptionalSubscriber();
            Container.DeclareSignal<ScoreChangedSignal>().OptionalSubscriber();
        }
    }
}