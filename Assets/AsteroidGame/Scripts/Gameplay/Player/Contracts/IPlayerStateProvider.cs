using AsteroidGame.Scripts.Domain.Player.Models;

namespace AsteroidGame.Scripts.Gameplay.Player.Contracts
{
    public interface IPlayerStateProvider
    {
        PlayerSnapshot Snapshot { get; }
    }
}