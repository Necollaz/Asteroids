using System;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.States;

namespace AsteroidGame.Scripts.UI.Player.Stats
{
    public sealed class PlayerStatsHudViewModelFactory
    {
        private const string PositionFormat = "X: {0:0.0}  Y: {1:0.0}";
        private const string RotationFormat = "Angle: {0:0.0}";
        private const string SpeedFormat = "Speed: {0:0.0}";
        private const string LaserChargesFormat = "Laser: {0}/{1}";
        private const string LaserCooldownReadyText = "Cooldown: Ready";
        private const string LaserCooldownFormat = "Cooldown: {0:0.0}s";

        public PlayerStatsHudViewModel Create(
            PlayerSnapshot snapshot,
            PlayerLaserRechargeState rechargeState,
            bool isVisible)
        {
            if (rechargeState == null)
                throw new ArgumentNullException(nameof(rechargeState));

            string cooldownText = rechargeState.IsRecharging
                ? string.Format(LaserCooldownFormat, rechargeState.RemainingSeconds)
                : LaserCooldownReadyText;

            return new PlayerStatsHudViewModel(
                isVisible,
                string.Format(PositionFormat, snapshot.Position.X, snapshot.Position.Y),
                string.Format(RotationFormat, snapshot.RotationDegrees),
                string.Format(SpeedFormat, snapshot.Speed),
                string.Format(LaserChargesFormat, snapshot.LaserCharges, snapshot.MaxLaserCharges),
                cooldownText);
        }
    }
}