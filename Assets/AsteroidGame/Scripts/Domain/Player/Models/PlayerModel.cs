using System;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.States;

namespace AsteroidGame.Scripts.Domain.Player.Models
{
    public sealed class PlayerModel
    {
        public PlayerModel(
            Body2D body,
            PlayerHealthState health,
            PlayerInvulnerabilityState invulnerability,
            PlayerLaserMagazine laserMagazine)
        {
            Body = body ?? throw new ArgumentNullException(nameof(body));
            Health = health ?? throw new ArgumentNullException(nameof(health));
            Invulnerability = invulnerability ?? throw new ArgumentNullException(nameof(invulnerability));
            LaserMagazine = laserMagazine ?? throw new ArgumentNullException(nameof(laserMagazine));
        }

        public Body2D Body { get; }
        public PlayerHealthState Health { get; }
        public PlayerInvulnerabilityState Invulnerability { get; }
        public PlayerLaserMagazine LaserMagazine { get; }
        public bool CanControl => !Health.IsDead && !Invulnerability.IsActive;
    }
}