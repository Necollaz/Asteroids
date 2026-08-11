using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Collision.Bodies
{
    public sealed class CollisionBody
    {
        public CollisionBody(CollisionCategory category, Body2D body, float radius)
        {
            Category = category;
            Body = body;
            Radius = radius;
            IsActive = true;
        }
        
        public CollisionCategory Category { get; }
        public Body2D Body { get; }
        public float Radius { get; }
        public bool IsActive { get; private set; }
        
        public void Activate() => IsActive = true;
        
        public void Deactivate() => IsActive = false;
    }
}