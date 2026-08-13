using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Presentation.Ufo.Effects
{
    public sealed class UfoExplosionInstance
    {
        private float _remainingSeconds;
        
        public UfoExplosionInstance(UfoExplosionView view) => View = view;
        
        public UfoExplosionView View { get; }

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
            _remainingSeconds = 0;
            View.Hide();
        }
    }
}