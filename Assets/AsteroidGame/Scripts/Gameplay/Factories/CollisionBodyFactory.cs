using Zenject;
using AsteroidGame.Scripts.Domain.Collision;
using AsteroidGame.Scripts.Domain.Physics;

namespace AsteroidGame.Scripts.Gameplay.Factories
{
    public sealed class CollisionBodyFactory : PlaceholderFactory<CollisionCategory, Body2D, float, CollisionBody>
    {
    }
}