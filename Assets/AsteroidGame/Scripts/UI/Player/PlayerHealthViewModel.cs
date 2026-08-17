using System;

namespace AsteroidGame.Scripts.UI.Player
{
    public readonly struct PlayerHealthViewModel
    {
        public PlayerHealthViewModel(bool isVisible, bool[] filledIcons)
        {
            IsVisible = isVisible;
            FilledIcons = filledIcons ?? throw new ArgumentNullException(nameof(filledIcons));
        }

        public bool IsVisible { get; }
        public bool[] FilledIcons { get; }
    }
}