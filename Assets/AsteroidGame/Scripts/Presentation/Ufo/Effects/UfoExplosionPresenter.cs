using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Signals.Enemies;

namespace AsteroidGame.Scripts.Presentation.Ufo.Effects
{
    public sealed class UfoExplosionPresenter : IInitializable, IDisposable
    {
        private readonly UfoExplosionPool _pool;
        private readonly SignalBus _signalBus;

        public UfoExplosionPresenter(UfoExplosionPool pool, SignalBus signalBus)
        {
            _pool = pool;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize() => _signalBus.Subscribe<EnemyDestroyedSignal>(HandleEnemyDestroyed);

        void IDisposable.Dispose() => _signalBus.Unsubscribe<EnemyDestroyedSignal>(HandleEnemyDestroyed);

        private void HandleEnemyDestroyed(EnemyDestroyedSignal signal)
        {
            if (signal.EnemyType != EnemyType.Ufo)
                return;
            
            _pool.Play(signal.Position);
        }
    }
}