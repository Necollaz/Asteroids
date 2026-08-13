using System;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Ufo.Settings;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Calculations
{
    public sealed class UfoKnockbackMovement
    {
        private readonly UfoSettings _settings;
        private readonly PhysicsValueFactory _physicsValueFactory;

        public UfoKnockbackMovement(UfoSettings settings, PhysicsValueFactory physicsValueFactory)
        {
            _settings = settings;
            _physicsValueFactory = physicsValueFactory;
        }

        public bool TryMove(UfoInstance ufo, float deltaSeconds, out Vector2D movementDirection)
        {
            movementDirection = default;
            
            if (!ufo.KnockbackState.IsActive)
                return false;
            
            Body2D body = ufo.Body;
            Vector2D velocity = ApplyDamping(body.Velocity.Value, deltaSeconds);
            Vector2D nextPosition = body.Position.Add(velocity.Multiply(deltaSeconds));
            body.SetVelocity(_physicsValueFactory.CreateVelocity(velocity));
            body.SetPosition(nextPosition);
            ufo.KnockbackState.Tick(deltaSeconds);
            
            movementDirection = velocity.Normalized;
            
            return true;
        }

        private Vector2D ApplyDamping(Vector2D velocity, float deltaSeconds)
        {
            float multiplier = Math.Max(0f, 1f - _settings.KnockbackDamping * deltaSeconds);
            
            return velocity.Multiply(multiplier);
        }
    }
}