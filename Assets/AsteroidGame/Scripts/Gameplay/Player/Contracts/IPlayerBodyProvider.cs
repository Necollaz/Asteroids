using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Gameplay.Player.Contracts
{
    public interface IPlayerBodyProvider
    {
        Body2D Body { get; }
    }
}