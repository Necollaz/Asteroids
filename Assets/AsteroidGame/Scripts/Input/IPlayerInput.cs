using AsteroidGame.Scripts.Domain.Player;

namespace AsteroidGame.Scripts.Input
{
    public interface IPlayerInput
    {
        PlayerInputState GetState();
    }
}