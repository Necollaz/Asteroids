using Zenject;
using AsteroidGame.Scripts.Domain.Asteroids.Models;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Gameplay.Asteroids.Contracts;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Factories
{
    public sealed class AsteroidInstanceZenjectFactory :
        PlaceholderFactory<AsteroidModel, CollisionBody, IAsteroidView, AsteroidInstance>
    {
    }
}