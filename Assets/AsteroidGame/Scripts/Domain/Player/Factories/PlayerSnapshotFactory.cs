using System;
using AsteroidGame.Scripts.Domain.Player.Models;

namespace AsteroidGame.Scripts.Domain.Player.Factories
{
    public sealed class PlayerSnapshotFactory
    {
        public PlayerSnapshot Create(PlayerModel playerModel)
        {
            if (playerModel == null)
                throw new ArgumentNullException(nameof(playerModel));

            return new PlayerSnapshot(
                playerModel.Body.Position,
                playerModel.Body.Velocity.Value,
                playerModel.Body.RotationDegrees,
                playerModel.Health.CurrentHealth,
                playerModel.Health.MaxHealth,
                playerModel.Invulnerability.IsActive,
                playerModel.LaserMagazine.Charges,
                playerModel.LaserMagazine.MaxCharges);
        }
    }
}