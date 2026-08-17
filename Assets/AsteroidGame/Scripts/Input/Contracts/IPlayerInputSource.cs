using AsteroidGame.Scripts.Domain.Player.Models;

namespace AsteroidGame.Scripts.Input.Contracts
{
    public interface IPlayerInputSource
    {
        PlayerInputSourceType SourceType { get; }
        
        PlayerInputState GetState();
    }
}