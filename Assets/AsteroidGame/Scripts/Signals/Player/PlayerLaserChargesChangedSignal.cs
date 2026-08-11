namespace AsteroidGame.Scripts.Signals.Player
{
    public sealed class PlayerLaserChargesChangedSignal
    {
        public PlayerLaserChargesChangedSignal(int charges, int maxCharges)
        {
            Charges = charges;
            MaxCharges = maxCharges;
        }

        public int Charges { get; }
        public int MaxCharges { get; }
    }
}