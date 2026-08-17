namespace AsteroidGame.Scripts.UI.Player
{
    public sealed class PlayerHealthHudSettings
    {
        private const float DefaultInitialVisibleSeconds = 7f;
        private const float DefaultDamageVisibleSeconds = 2f;

        public float InitialVisibleSeconds => DefaultInitialVisibleSeconds;
        public float DamageVisibleSeconds => DefaultDamageVisibleSeconds;
    }
}