using AsteroidGame.Scripts.Domain.Player.Models;

namespace AsteroidGame.Scripts.Input
{
    public interface IPlayerInput
    {
        PlayerInputState GetState();
    }
}