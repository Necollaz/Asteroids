using System;
using AsteroidGame.Scripts.Domain.Player.Settings;

namespace AsteroidGame.Scripts.Domain.Player.States
{
    public sealed class PlayerHealthState
    {
        public PlayerHealthState(PlayerCollisionSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            MaxHealth = settings.MaxHealth;
            CurrentHealth = MaxHealth;
        }

        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; }
        public bool IsDead => CurrentHealth <= 0;

        public bool ApplyDamage(int damage)
        {
            if (damage <= 0 || IsDead)
                return false;

            CurrentHealth -= damage;

            if (CurrentHealth < 0)
                CurrentHealth = 0;

            return true;
        }

        public void RestoreFull() => CurrentHealth = MaxHealth;
    }
}