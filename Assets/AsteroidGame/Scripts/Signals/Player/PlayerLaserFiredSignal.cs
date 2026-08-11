namespace AsteroidGame.Scripts.Signals.Player
{
    public sealed class PlayerLaserFiredSignal
    {
        public PlayerLaserFiredSignal(
            float visualWidth,
            float visibleSeconds)
        {
            VisualWidth = visualWidth;
            VisibleSeconds = visibleSeconds;
        }
        
        public float VisualWidth { get; }
        public float VisibleSeconds { get; }
    }
}