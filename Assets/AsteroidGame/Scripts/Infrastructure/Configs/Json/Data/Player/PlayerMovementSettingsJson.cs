using System;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player
{
    [Serializable]
    public sealed class PlayerMovementSettingsJson
    {
        public float SpawnPositionX;
        public float SpawnPositionY;
        public float Acceleration;
        public float TurnSpeed;
        public float MaxSpeed;
        public float LinearDamping;
        public float SpawnRotationDegrees;
    }
}