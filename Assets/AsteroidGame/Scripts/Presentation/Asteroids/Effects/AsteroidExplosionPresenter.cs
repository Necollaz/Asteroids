using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Signals.Enemies;

namespace AsteroidGame.Scripts.Presentation.Asteroids.Effects
{
    public sealed class AsteroidExplosionPresenter : IInitializable, IDisposable
    {
        private readonly AsteroidExplosionPool _pool;
        private readonly SignalBus _signalBus;

        public AsteroidExplosionPresenter(AsteroidExplosionPool pool, SignalBus signalBus)
        {
            _pool = pool;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize() => _signalBus.Subscribe<EnemyDestroyedSignal>(HandleEnemyDestroyed);

        void IDisposable.Dispose() => _signalBus.Unsubscribe<EnemyDestroyedSignal>(HandleEnemyDestroyed);

        private void HandleEnemyDestroyed(EnemyDestroyedSignal signal)
        {
            if (!IsAsteroid(signal.EnemyType))
                return;

            _pool.Play(signal.Position);
        }

        private bool IsAsteroid(EnemyType enemyType) =>
            enemyType == EnemyType.LargeAsteroid ||
            enemyType == EnemyType.MediumAsteroid ||
            enemyType == EnemyType.SmallAsteroid;
    }
}