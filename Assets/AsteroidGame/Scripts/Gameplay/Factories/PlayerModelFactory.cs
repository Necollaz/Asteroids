using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Models;

namespace AsteroidGame.Scripts.Gameplay.Factories
{
    public sealed class PlayerModelFactory : PlaceholderFactory<Body2D, PlayerModel>
    {
    }
}