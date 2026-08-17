using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Presentation.Common.Effects
{
    public interface IPooledTimedEffect
    {
        bool Tick(float deltaTime);

        void Play(Vector2D position);

        void Hide();
    }
}