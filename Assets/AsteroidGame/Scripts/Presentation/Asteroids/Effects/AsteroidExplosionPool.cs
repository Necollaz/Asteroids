using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.Presentation.Asteroids.Effects
{
    public sealed class AsteroidExplosionPool : IInitializable, ITickable
    {
        private const int PoolSize = 8;
        
        private readonly AsteroidExplosionViewPrefabFactory _viewFactory;
        private readonly AsteroidExplosionInstanceFactory _instanceFactory;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;
        private readonly Queue<AsteroidExplosionInstance> _availableEffects = new();
        private readonly List<AsteroidExplosionInstance> _activeEffects = new();

        public AsteroidExplosionPool(
            AsteroidExplosionViewPrefabFactory viewFactory,
            AsteroidExplosionInstanceFactory instanceFactory,
            ITimeProvider timeProvider,
            IGameplayPauseState pauseState)
        {
            _viewFactory = viewFactory;
            _instanceFactory = instanceFactory;
            _timeProvider = timeProvider;
            _pauseState = pauseState;
        }

        void IInitializable.Initialize()
        {
            for (int i = 0; i < PoolSize; i++)
                _availableEffects.Enqueue(CreateInstance());
        }

        void ITickable.Tick()
        {
            if (_pauseState.IsPaused)
                return;
            
            float deltaTime = _timeProvider.DeltaTime;

            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                AsteroidExplosionInstance effect = _activeEffects[i];
                
                if (!effect.Tick(deltaTime))
                    continue;

                Release(effect);
            }
        }

        public void Play(Vector2D position)
        {
            if (_availableEffects.Count == 0)
                return;
            
            AsteroidExplosionInstance effect = _availableEffects.Dequeue();
            _activeEffects.Add(effect);
            effect.Play(position);
        }

        private AsteroidExplosionInstance CreateInstance()
        {
            AsteroidExplosionView view = _viewFactory.Create();
            AsteroidExplosionInstance instance = _instanceFactory.Create(view);
            instance.Hide();

            return instance;
        }

        private void Release(AsteroidExplosionInstance effect)
        {
            _activeEffects.Remove(effect);
            effect.Hide();
            _availableEffects.Enqueue(effect);
        }
    }
}