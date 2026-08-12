using System;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Presentation.Asteroids.Effects
{
    public sealed class AsteroidExplosionInstance
    {
        public AsteroidExplosionInstance(AsteroidExplosionView  view) => 
            View = view ?? throw new ArgumentNullException(nameof(view));
        
        public AsteroidExplosionView View { get; }
        public float RemainingSeconds { get; private set; }

        public bool Tick(float deltaTime)
        {
            RemainingSeconds -= deltaTime;
            
            return RemainingSeconds <= 0f;
        }
        
        public void Play(Vector2D position)
        {
            RemainingSeconds = View.DurationSeconds;
            View.Play(position.X, position.Y);
        }

        public void Hide()
        {
            RemainingSeconds = 0;
            View.Hide();
        }
    }
}