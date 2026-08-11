using System;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.World.Bounds;

namespace AsteroidGame.Scripts.Domain.Physics.Services
{
    public sealed class CustomPhysicsWorld
    {
        private readonly WorldBounds _worldBounds;
        private readonly PhysicsValueFactory _physicsValueFactory;

        public CustomPhysicsWorld(WorldBounds worldBounds, PhysicsValueFactory physicsValueFactory)
        {
            _worldBounds = worldBounds;
            _physicsValueFactory = physicsValueFactory;
        }

        public void Step(Body2D body, Acceleration acceleration, float deltaTime, float maxSpeed, float linearDamping)
        {
            if (deltaTime <= 0f)
                return;

            Vector2D velocity = body.Velocity.Value
                .Add(acceleration.Value.Multiply(deltaTime))
                .ClampMagnitude(maxSpeed);

            if (acceleration.Value.SqrMagnitude <= float.Epsilon && linearDamping > 0f)
                velocity = ApplyDamping(velocity, linearDamping, deltaTime);

            Vector2D nextPosition = body.Position.Add(velocity.Multiply(deltaTime));
            nextPosition = _worldBounds.WrapPosition(nextPosition);

            body.SetVelocity(_physicsValueFactory.CreateVelocity(velocity));
            body.SetPosition(nextPosition);
        }
        
        public void ApplyBounce(Body2D first, Body2D second, float bounceSpeed)
        {
            Vector2D difference = first.Position.Subtract(second.Position);
            Vector2D normal = difference.SqrMagnitude <= float.Epsilon
                ? _physicsValueFactory.CreateVector(0f, 1f)
                : difference.Normalized;

            first.SetVelocity(_physicsValueFactory.CreateVelocity(normal.Multiply(bounceSpeed)));
            second.SetVelocity(_physicsValueFactory.CreateVelocity(normal.Multiply(-bounceSpeed)));
        }

        private Vector2D ApplyDamping(Vector2D velocity, float linearDamping, float deltaTime)
        {
            float multiplier = Math.Max(0f, 1f - linearDamping * deltaTime);

            return velocity.Multiply(multiplier);
        }
    }
}