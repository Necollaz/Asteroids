using System;
using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.Presentation.Asteroids.Effects
{
    public sealed class AsteroidExplosionPool : IInitializable, ITickable
    {
        private const int PoolSize = 32;
        
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
            AsteroidExplosionInstance effect = GetAvailableEffect();
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
        
        private AsteroidExplosionInstance GetAvailableEffect()
        {
            if (_availableEffects.Count > 0)
                return _availableEffects.Dequeue();

            if (_activeEffects.Count == 0)
                throw new InvalidOperationException("Asteroid explosion pool has no effects.");

            AsteroidExplosionInstance oldestEffect = _activeEffects[0];
            _activeEffects.RemoveAt(0);
            oldestEffect.Hide();

            return oldestEffect;
        }

        private void Release(AsteroidExplosionInstance effect)
        {
            if (!_activeEffects.Remove(effect))
                throw new InvalidOperationException("Asteroid explosion effect is already released.");

            effect.Hide();
            _availableEffects.Enqueue(effect);
        }
    }
}