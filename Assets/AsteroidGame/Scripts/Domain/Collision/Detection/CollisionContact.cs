using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Collision.Detection
{
    public readonly struct CollisionContact
    {
        public CollisionContact(CollisionBody first, CollisionBody second, Vector2D normal)
        {
            First = first;
            Second = second;
            Normal = normal;
        }
        
        public CollisionBody First { get; }
        public CollisionBody Second { get; }
        public Vector2D Normal { get; }
    }
}