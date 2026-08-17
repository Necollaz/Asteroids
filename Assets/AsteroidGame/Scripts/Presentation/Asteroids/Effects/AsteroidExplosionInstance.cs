using System;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Presentation.Common.Effects;

namespace AsteroidGame.Scripts.Presentation.Asteroids.Effects
{
    public sealed class AsteroidExplosionInstance : IPooledTimedEffect
    {
        private float _remainingSeconds;
        
        public AsteroidExplosionInstance(AsteroidExplosionView  view) => 
            View = view ?? throw new ArgumentNullException(nameof(view));
        
        public AsteroidExplosionView View { get; }

        public bool Tick(float deltaTime)
        {
            _remainingSeconds -= deltaTime;

            return _remainingSeconds <= 0f;
        }

        public void Play(Vector2D position)
        {
            _remainingSeconds = View.DurationSeconds;
            View.Play(position);
        }

        public void Hide()
        {
            _remainingSeconds = 0f;
            View.Hide();
        }
    }
}