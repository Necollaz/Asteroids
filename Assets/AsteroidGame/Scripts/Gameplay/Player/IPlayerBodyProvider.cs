using AsteroidGame.Scripts.Domain.Physics;

namespace AsteroidGame.Scripts.Gameplay.Player
{
    public interface IPlayerBodyProvider
    {
        Body2D Body { get; }
    }
}