using AsteroidGame.Scripts.Domain.Player;

namespace AsteroidGame.Scripts.Gameplay.Player
{
    public interface IPlayerStateProvider
    {
        PlayerSnapshot Snapshot { get; }
    }
}