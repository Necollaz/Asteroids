using Zenject;
using AsteroidGame.Scripts.Domain.Player;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Gameplay.Player.Services
{
    public sealed class PlayerHealthService
    {
        private readonly SignalBus _signalBus;

        public PlayerHealthService(PlayerCollisionSettings settings, SignalBus signalBus)
        {
            _signalBus = signalBus;
            CurrentHealth = settings.MaxHealth;
        }
        
        public int CurrentHealth { get; private set; }

        public void ApplyDamage(int damage)
        {
            if (damage <= 0 || CurrentHealth <= 0)
                return;
            
            CurrentHealth -= damage;
            
            if (CurrentHealth < 0)
                CurrentHealth = 0;
            
            _signalBus.Fire<PlayerDamagedSignal>();
        }
    }
}