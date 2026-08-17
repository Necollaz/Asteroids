using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Player.Contracts;
using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Validation
{
    public sealed class PlayerSettingsValidator
    {
        public void Validate(
            IPlayerMovementSettingsData movement,
            IPlayerCollisionSettingsData collision,
            IKeyboardInputSettingsData keyboard,
            IMobileInputSettingsData mobile)
        {
            ValidateMovement(movement);
            ValidateCollision(collision);
            ValidateKeyboard(keyboard);
            ValidateMobile(mobile);
        }

        private void ValidateMovement(IPlayerMovementSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.PlayerAcceleration <= 0f)
                throw new InvalidOperationException("Player acceleration must be greater than zero.");

            if (settings.PlayerTurnSpeed <= 0f)
                throw new InvalidOperationException("Player turn speed must be greater than zero.");

            if (settings.PlayerMaxSpeed <= 0f)
                throw new InvalidOperationException("Player max speed must be greater than zero.");

            if (settings.PlayerLinearDamping < 0f)
                throw new InvalidOperationException("Player linear damping cannot be negative.");
        }

        private void ValidateCollision(IPlayerCollisionSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.PlayerMaxHealth <= 0)
                throw new InvalidOperationException("Player max health must be greater than zero.");

            if (settings.PlayerCollisionRadius <= 0f)
                throw new InvalidOperationException("Player collision radius must be greater than zero.");

            if (settings.PlayerCollisionBounceSpeed <= 0f)
                throw new InvalidOperationException("Player collision bounce speed must be greater than zero.");

            if (settings.PlayerInvulnerabilitySeconds <= 0f)
                throw new InvalidOperationException("Player invulnerability seconds must be greater than zero.");
        }

        private void ValidateKeyboard(IKeyboardInputSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            ValidateKey(settings.TurnLeftKey, nameof(settings.TurnLeftKey));
            ValidateKey(settings.TurnRightKey, nameof(settings.TurnRightKey));
            ValidateKey(settings.ThrustKey, nameof(settings.ThrustKey));
            ValidateKey(settings.FireBulletKey, nameof(settings.FireBulletKey));
            ValidateKey(settings.FireLaserKey, nameof(settings.FireLaserKey));
            ValidateKey(settings.AlternativeTurnLeftKey, nameof(settings.AlternativeTurnLeftKey));
            ValidateKey(settings.AlternativeTurnRightKey, nameof(settings.AlternativeTurnRightKey));
            ValidateKey(settings.AlternativeThrustKey, nameof(settings.AlternativeThrustKey));
            ValidateKey(settings.AlternativeFireBulletKey, nameof(settings.AlternativeFireBulletKey));
            ValidateKey(settings.AlternativeFireLaserKey, nameof(settings.AlternativeFireLaserKey));
        }

        private void ValidateMobile(IMobileInputSettingsData settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            if (settings.MobileTurnLeftValue < -1f || settings.MobileTurnLeftValue > 1f)
                throw new InvalidOperationException("Mobile turn left value must be between -1 and 1.");

            if (settings.MobileTurnRightValue < -1f || settings.MobileTurnRightValue > 1f)
                throw new InvalidOperationException("Mobile turn right value must be between -1 and 1.");

            if (settings.MobileTurnLeftValue == 0f)
                throw new InvalidOperationException("Mobile turn left value cannot be zero.");

            if (settings.MobileTurnRightValue == 0f)
                throw new InvalidOperationException("Mobile turn right value cannot be zero.");
        }

        private void ValidateKey(KeyCode keyCode, string propertyName)
        {
            if (keyCode == KeyCode.None)
                throw new InvalidOperationException($"{propertyName} must be assigned.");
        }
    }
}