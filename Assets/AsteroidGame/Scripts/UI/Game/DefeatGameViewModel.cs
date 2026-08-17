namespace AsteroidGame.Scripts.UI.Game
{
    public readonly struct DefeatGameViewModel
    {
        public DefeatGameViewModel(bool isVisible) => IsVisible = isVisible;

        public bool IsVisible { get; }
    }
}