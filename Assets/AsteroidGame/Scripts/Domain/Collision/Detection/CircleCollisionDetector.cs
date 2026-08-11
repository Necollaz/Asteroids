using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Collision.Detection
{
    public sealed class CircleCollisionDetector
    {
        private readonly PhysicsValueFactory _physicsFactory;
        
        public CircleCollisionDetector(PhysicsValueFactory physicsFactory) => _physicsFactory = physicsFactory;

        public bool TryDetect(CollisionBody first, CollisionBody second, out CollisionContact contact)
        {
            contact = default;
            
            if (!first.IsActive || !second.IsActive)
                return false;
            
            Vector2D difference = first.Body.Position.Subtract(second.Body.Position);
            float radiusSum = first.Radius + second.Radius;
            
            if (difference.SqrMagnitude > radiusSum * radiusSum)
                return false;

            Vector2D normal = difference.SqrMagnitude <= float.Epsilon
                ? _physicsFactory.CreateVector(0f, 1f)
                : difference.Normalized;
            contact = new CollisionContact(first, second, normal);
            
            return true;
        }
    }
}