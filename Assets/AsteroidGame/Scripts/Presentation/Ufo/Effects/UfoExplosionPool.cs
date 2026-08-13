using System;
using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Presentation.Ufo.Effects.Factories;

namespace AsteroidGame.Scripts.Presentation.Ufo.Effects
{
    public sealed class UfoExplosionPool : IInitializable, ITickable
    {
        private const int PoolSize = 10;
        
        private readonly UfoExplosionViewPrefabFactory _viewFactory;
        private readonly UfoExplosionInstanceFactory _instanceFactory;
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;
        private readonly Queue<UfoExplosionInstance> _availableEffects = new();
        private readonly List<UfoExplosionInstance> _activeEffects = new();
        
        public UfoExplosionPool(
            UfoExplosionViewPrefabFactory viewFactory,
            UfoExplosionInstanceFactory instanceFactory,
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
                UfoExplosionInstance effect = _activeEffects[i];
                
                if (!effect.Tick(deltaTime))
                    continue;

                Release(effect);
            }
        }

        public void Play(Vector2D position)
        {
            UfoExplosionInstance effect = GetAvailableEffect();
            _activeEffects.Add(effect);
            effect.Play(position);
        }

        private UfoExplosionInstance CreateInstance()
        {
            UfoExplosionView view = _viewFactory.Create();
            UfoExplosionInstance instance = _instanceFactory.Create(view);
            instance.Hide();
            
            return instance;
        }

        private UfoExplosionInstance GetAvailableEffect()
        {
            if (_availableEffects.Count > 0)
                return _availableEffects.Dequeue();

            if (_activeEffects.Count == 0)
                throw new InvalidOperationException("UFO explosion pool has no effects.");

            UfoExplosionInstance oldestEffect = _activeEffects[0];
            _activeEffects.RemoveAt(0);
            oldestEffect.Hide();

            return oldestEffect;
        }
        
        private void Release(UfoExplosionInstance effect)
        {
            if (!_activeEffects.Remove(effect))
                throw new InvalidOperationException("UFO explosion effect is already released.");

            effect.Hide();
            _availableEffects.Enqueue(effect);
        }
    }
}