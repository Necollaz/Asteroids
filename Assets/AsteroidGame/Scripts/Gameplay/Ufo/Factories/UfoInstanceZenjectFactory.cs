using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Contracts;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.States;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Factories
{
    public sealed class UfoInstanceZenjectFactory :
        PlaceholderFactory<UfoModel, CollisionBody, IUfoView, UfoKnockbackState, UfoInstance>
    {
    }
}