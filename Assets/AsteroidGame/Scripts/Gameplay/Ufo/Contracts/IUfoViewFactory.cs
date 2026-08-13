using AsteroidGame.Scripts.Domain.Ufo.Contracts;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Contracts
{
    public interface IUfoViewFactory
    {
        IUfoView Create();
    }
}