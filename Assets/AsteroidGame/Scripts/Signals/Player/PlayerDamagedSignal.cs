namespace AsteroidGame.Scripts.Signals.Player
{
    public sealed class PlayerDamagedSignal
    {
        public PlayerDamagedSignal(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }

        public int CurrentHealth { get; }
        public int MaxHealth { get; }
    }
}