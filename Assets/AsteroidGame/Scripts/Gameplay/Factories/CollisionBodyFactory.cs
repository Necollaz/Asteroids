using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Gameplay.Factories
{
    public sealed class CollisionBodyFactory : PlaceholderFactory<CollisionCategory, Body2D, float, CollisionBody>
    {
    }
}