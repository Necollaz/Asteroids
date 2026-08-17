using System;

namespace AsteroidGame.Scripts.UI.Player
{
    public sealed class PlayerHealthViewModelFactory
    {
        public PlayerHealthViewModel Create(int currentHealth, int maxHealth, bool isVisible)
        {
            if (currentHealth < 0)
                throw new ArgumentOutOfRangeException(nameof(currentHealth));

            if (maxHealth <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxHealth));

            bool[] filledIcons = new bool[maxHealth];

            for (int i = 0; i < filledIcons.Length; i++)
                filledIcons[i] = i < currentHealth;

            return new PlayerHealthViewModel(isVisible, filledIcons);
        }
    }
}