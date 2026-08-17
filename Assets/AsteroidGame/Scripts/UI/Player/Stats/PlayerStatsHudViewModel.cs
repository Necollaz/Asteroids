namespace AsteroidGame.Scripts.UI.Player.Stats
{
    public readonly struct PlayerStatsHudViewModel
    {
        public PlayerStatsHudViewModel(
            bool isVisible,
            string positionText,
            string rotationText,
            string speedText,
            string laserChargesText,
            string laserCooldownText)
        {
            IsVisible = isVisible;
            PositionText = positionText;
            RotationText = rotationText;
            SpeedText = speedText;
            LaserChargesText = laserChargesText;
            LaserCooldownText = laserCooldownText;
        }

        public bool IsVisible { get; }
        public string PositionText { get; }
        public string RotationText { get; }
        public string SpeedText { get; }
        public string LaserChargesText { get; }
        public string LaserCooldownText { get; }
    }
}