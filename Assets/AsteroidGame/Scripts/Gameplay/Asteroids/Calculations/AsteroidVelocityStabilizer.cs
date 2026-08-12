using UnityEngine;
using AsteroidGame.Scripts.Domain.Asteroids.Settings;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Calculations
{
    public sealed class AsteroidVelocityStabilizer
    {
        private readonly AsteroidSettings _settings;
        private readonly PhysicsValueFactory _factory;

        public AsteroidVelocityStabilizer(AsteroidSettings settings, PhysicsValueFactory factory)
        {
            _settings = settings;
            _factory = factory;
        }

        public void Stabilize(AsteroidInstance asteroid, float deltaTime)
        {
            Body2D body = asteroid.Body;
            Vector2D velocity = body.Velocity.Value;
            float currentSpeed = velocity.Magnitude;
            
            if (currentSpeed <= float.Epsilon)
                return;

            float targetSpeed = _settings.GetSpeed(asteroid.Type);
            float nextSpeed = MoveTowards(currentSpeed, targetSpeed, _settings.SpeedReturnRate * deltaTime);
            Vector2D nextVelocity = velocity.Normalized.Multiply(nextSpeed);
            body.SetVelocity(_factory.CreateVelocity(nextVelocity));
        }

        private float MoveTowards(float current, float target, float maxDelta)
        {
            if (Mathf.Abs(target - current) <= maxDelta)
                return target;
            
            return current < target ? current + maxDelta : current - maxDelta;
        }
    }
}