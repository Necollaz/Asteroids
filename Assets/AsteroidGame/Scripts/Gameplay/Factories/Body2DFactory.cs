using Zenject;
using AsteroidGame.Scripts.Domain.Physics;

namespace AsteroidGame.Scripts.Gameplay.Factories
{
    public sealed class Body2DFactory : PlaceholderFactory<Vector2D, Velocity, float, Body2D>
    {
    }
}