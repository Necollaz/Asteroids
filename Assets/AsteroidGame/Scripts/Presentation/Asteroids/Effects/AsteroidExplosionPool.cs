using AsteroidGame.Scripts.Domain.Enemies.Settings;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Presentation.Asteroids.Effects.Factories;
using AsteroidGame.Scripts.Presentation.Common.Effects;

namespace AsteroidGame.Scripts.Presentation.Asteroids.Effects
{
    public sealed class AsteroidExplosionPool : TimedEffectPool<AsteroidExplosionInstance>
    {
        private const string EmptyMessage = "Asteroid explosion pool has no effects.";
        private const string AlreadyReleasedMessageText = "Asteroid explosion effect is already released.";
        
        private readonly AsteroidExplosionViewPrefabFactory _viewFactory;
        private readonly AsteroidExplosionInstanceFactory _instanceFactory;
        private readonly EnemySpawnSettings _spawnSettings;

        public AsteroidExplosionPool(
            AsteroidExplosionViewPrefabFactory viewFactory,
            AsteroidExplosionInstanceFactory instanceFactory,
            EnemySpawnSettings spawnSettings,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState)
            : base(timeProvider, pauseState)
        {
            _viewFactory = viewFactory;
            _instanceFactory = instanceFactory;
            _spawnSettings = spawnSettings;
        }

        protected override int PoolSize => _spawnSettings.AsteroidExplosionPoolSize;
        protected override string EmptyPoolMessage => EmptyMessage;
        protected override string AlreadyReleasedMessage => AlreadyReleasedMessageText;

        protected override AsteroidExplosionInstance CreateInstance()
        {
            AsteroidExplosionView view = _viewFactory.Create();

            return _instanceFactory.Create(view);
        }
    }
}