using AsteroidGame.Scripts.Domain.Enemies.Settings;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Presentation.Common.Effects;
using AsteroidGame.Scripts.Presentation.Ufo.Effects.Factories;

namespace AsteroidGame.Scripts.Presentation.Ufo.Effects
{
    public sealed class UfoExplosionPool : TimedEffectPool<UfoExplosionInstance>
    {
        private const string EmptyMessage = "UFO explosion pool has no effects.";
        private const string AlreadyReleasedMessageText = "UFO explosion effect is already released.";

        private readonly UfoExplosionViewPrefabFactory _viewFactory;
        private readonly UfoExplosionInstanceFactory _instanceFactory;
        private readonly EnemySpawnSettings _spawnSettings;

        public UfoExplosionPool(
            UfoExplosionViewPrefabFactory viewFactory,
            UfoExplosionInstanceFactory instanceFactory,
            EnemySpawnSettings spawnSettings,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState)
            : base(timeProvider, pauseState)
        {
            _viewFactory = viewFactory;
            _instanceFactory = instanceFactory;
            _spawnSettings = spawnSettings;
        }

        protected override int PoolSize => _spawnSettings.UfoExplosionPoolSize;

        protected override string EmptyPoolMessage => EmptyMessage;

        protected override string AlreadyReleasedMessage => AlreadyReleasedMessageText;

        protected override UfoExplosionInstance CreateInstance()
        {
            UfoExplosionView view = _viewFactory.Create();

            return _instanceFactory.Create(view);
        }
    }
}