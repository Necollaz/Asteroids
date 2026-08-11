using System;
using AsteroidGame.Scripts.Domain.Player.Settings;

namespace AsteroidGame.Scripts.Domain.Player.States
{
    public sealed class PlayerLaserMagazine
    {
        public PlayerLaserMagazine(PlayerLaserSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            MaxCharges = settings.MaxLaserCharges;
            Charges = settings.InitialLaserCharges;
        }

        public int Charges { get; private set; }
        public int MaxCharges { get; }
        public bool HasCharges => Charges > 0;
        public bool IsFull => Charges >= MaxCharges;

        public bool ConsumeCharge()
        {
            if (!HasCharges)
                return false;

            Charges--;

            return true;
        }

        public bool TryAddCharge()
        {
            if (IsFull)
                return false;

            Charges++;

            return true;
        }

        public void Refill() => Charges = MaxCharges;
    }
}