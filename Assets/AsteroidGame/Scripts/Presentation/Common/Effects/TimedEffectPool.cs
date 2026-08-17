using System;
using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.Presentation.Common.Effects
{
    public abstract class TimedEffectPool<TEffect> : IInitializable, ITickable
        where TEffect : IPooledTimedEffect
    {
        private readonly ITimeProvider _timeProvider;
        private readonly IGameplayPauseState _pauseState;
        private readonly Queue<TEffect> _availableEffects = new();
        private readonly List<TEffect> _activeEffects = new();

        protected TimedEffectPool(ITimeProvider timeProvider, IGameplayPauseState pauseState)
        {
            _timeProvider = timeProvider;
            _pauseState = pauseState;
        }

        protected abstract int PoolSize { get; }
        protected abstract string EmptyPoolMessage { get; }
        protected abstract string AlreadyReleasedMessage { get; }

        void IInitializable.Initialize()
        {
            for (int i = 0; i < PoolSize; i++)
                _availableEffects.Enqueue(CreateHiddenInstance());
        }

        void ITickable.Tick()
        {
            if (_pauseState.IsPaused)
                return;

            float deltaTime = _timeProvider.DeltaTime;

            for (int i = _activeEffects.Count - 1; i >= 0; i--)
            {
                TEffect effect = _activeEffects[i];

                if (!effect.Tick(deltaTime))
                    continue;

                Release(effect);
            }
        }
        
        protected abstract TEffect CreateInstance();

        public void Play(Vector2D position)
        {
            TEffect effect = GetAvailableEffect();
            _activeEffects.Add(effect);
            effect.Play(position);
        }

        private TEffect CreateHiddenInstance()
        {
            TEffect effect = CreateInstance();
            effect.Hide();

            return effect;
        }

        private TEffect GetAvailableEffect()
        {
            if (_availableEffects.Count > 0)
                return _availableEffects.Dequeue();

            if (_activeEffects.Count == 0)
                throw new InvalidOperationException(EmptyPoolMessage);

            TEffect oldestEffect = _activeEffects[0];
            _activeEffects.RemoveAt(0);
            oldestEffect.Hide();

            return oldestEffect;
        }

        private void Release(TEffect effect)
        {
            if (!_activeEffects.Remove(effect))
                throw new InvalidOperationException(AlreadyReleasedMessage);

            effect.Hide();
            _availableEffects.Enqueue(effect);
        }
    }
}