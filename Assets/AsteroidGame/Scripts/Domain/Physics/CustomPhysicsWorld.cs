using System;
using AsteroidGame.Scripts.Domain.World;

namespace AsteroidGame.Scripts.Domain.Physics
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

        private Vector2D ApplyDamping(Vector2D velocity, float linearDamping, float deltaTime)
        {
            float multiplier = Math.Max(0f, 1f - linearDamping * deltaTime);

            return velocity.Multiply(multiplier);
        }
    }
}