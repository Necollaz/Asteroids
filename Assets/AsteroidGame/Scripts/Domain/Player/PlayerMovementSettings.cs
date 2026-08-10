using System;
using AsteroidGame.Scripts.Domain.Physics;

namespace AsteroidGame.Scripts.Domain.Player
{
    public sealed class PlayerMovementSettings
    {
        public PlayerMovementSettings(
            PhysicsValueFactory physicsValueFactory,
            IPlayerMovementSettingsData settingsData)
        {
            if (settingsData.PlayerAcceleration <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerAcceleration));

            if (settingsData.PlayerTurnSpeed <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerTurnSpeed));

            if (settingsData.PlayerMaxSpeed <= 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerMaxSpeed));

            if (settingsData.PlayerLinearDamping < 0f)
                throw new ArgumentOutOfRangeException(nameof(settingsData.PlayerLinearDamping));

            Acceleration = settingsData.PlayerAcceleration;
            TurnSpeedDegreesPerSecond = settingsData.PlayerTurnSpeed;
            MaxSpeed = settingsData.PlayerMaxSpeed;
            LinearDamping = settingsData.PlayerLinearDamping;
            SpawnRotationDegrees = settingsData.PlayerSpawnRotationDegrees;
            SpawnPosition = physicsValueFactory.CreateVector(
                settingsData.PlayerSpawnPositionX, 
                settingsData.PlayerSpawnPositionY);
            Vector2D zeroVector = physicsValueFactory.CreateVector(0f, 0f);
            InitialVelocity = physicsValueFactory.CreateVelocity(zeroVector);
            NoAcceleration = physicsValueFactory.CreateAcceleration(zeroVector);
        }

        public Acceleration NoAcceleration { get; }
        public Vector2D SpawnPosition { get; }
        public Velocity InitialVelocity { get; }
        public float Acceleration { get; }
        public float TurnSpeedDegreesPerSecond { get; }
        public float MaxSpeed { get; }
        public float LinearDamping { get; }
        public float SpawnRotationDegrees { get; }
    }
}