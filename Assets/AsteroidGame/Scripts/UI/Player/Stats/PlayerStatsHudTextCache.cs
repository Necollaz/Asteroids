using System;
using System.Globalization;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.States;

namespace AsteroidGame.Scripts.UI.Player.Stats
{
    public sealed class PlayerStatsHudTextCache
    {
        private const float DisplayPrecisionMultiplier = 10f;
        private const string LaserCooldownReadyText = "Cooldown: Ready";

        private int _positionX;
        private int _positionY;
        private int _rotation;
        private int _speed;
        private int _laserCharges;
        private int _maxLaserCharges;
        private int _laserCooldown;

        private string _positionText = string.Empty;
        private string _rotationText = string.Empty;
        private string _speedText = string.Empty;
        private string _laserChargesText = string.Empty;
        private string _laserCooldownText = LaserCooldownReadyText;

        private bool _isLaserRecharging;
        private bool _hasCachedValues;

        public string PositionText => _positionText;
        public string RotationText => _rotationText;
        public string SpeedText => _speedText;
        public string LaserChargesText => _laserChargesText;
        public string LaserCooldownText => _laserCooldownText;

        public void Update(PlayerSnapshot snapshot, PlayerLaserRechargeState rechargeState)
        {
            if (rechargeState == null)
                throw new ArgumentNullException(nameof(rechargeState));

            UpdatePosition(snapshot);
            UpdateRotation(snapshot);
            UpdateSpeed(snapshot);
            UpdateLaserCharges(snapshot);
            UpdateLaserCooldown(rechargeState);

            _hasCachedValues = true;
        }

        private void UpdatePosition(PlayerSnapshot snapshot)
        {
            int positionX = ToDisplayTenths(snapshot.Position.X);
            int positionY = ToDisplayTenths(snapshot.Position.Y);

            if (_hasCachedValues && _positionX == positionX && _positionY == positionY)
                return;

            _positionX = positionX;
            _positionY = positionY;
            _positionText = $"X: {FormatTenths(positionX)}  Y: {FormatTenths(positionY)}";
        }

        private void UpdateRotation(PlayerSnapshot snapshot)
        {
            int rotation = ToDisplayTenths(snapshot.RotationDegrees);

            if (_hasCachedValues && _rotation == rotation)
                return;

            _rotation = rotation;
            _rotationText = $"Angle: {FormatTenths(rotation)}";
        }

        private void UpdateSpeed(PlayerSnapshot snapshot)
        {
            int speed = ToDisplayTenths(snapshot.Speed);

            if (_hasCachedValues && _speed == speed)
                return;

            _speed = speed;
            _speedText = $"Speed: {FormatTenths(speed)}";
        }

        private void UpdateLaserCharges(PlayerSnapshot snapshot)
        {
            if (_hasCachedValues &&
                _laserCharges == snapshot.LaserCharges &&
                _maxLaserCharges == snapshot.MaxLaserCharges)
            {
                return;
            }

            _laserCharges = snapshot.LaserCharges;
            _maxLaserCharges = snapshot.MaxLaserCharges;
            _laserChargesText = $"Laser: {_laserCharges}/{_maxLaserCharges}";
        }

        private void UpdateLaserCooldown(PlayerLaserRechargeState rechargeState)
        {
            int cooldown = ToDisplayTenths(rechargeState.RemainingSeconds);

            if (_hasCachedValues &&
                _isLaserRecharging == rechargeState.IsRecharging &&
                _laserCooldown == cooldown)
            {
                return;
            }

            _isLaserRecharging = rechargeState.IsRecharging;
            _laserCooldown = cooldown;
            _laserCooldownText = rechargeState.IsRecharging 
                ? $"Cooldown: {FormatTenths(cooldown)}s"
                : LaserCooldownReadyText;
        }

        private int ToDisplayTenths(float value) =>
            (int)Math.Round(value * DisplayPrecisionMultiplier, MidpointRounding.AwayFromZero);

        private string FormatTenths(int tenths) =>
            (tenths / DisplayPrecisionMultiplier).ToString("0.0", CultureInfo.InvariantCulture);
    }
}