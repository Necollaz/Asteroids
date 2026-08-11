using System;
using System.Collections.Generic;

namespace AsteroidGame.Scripts.Domain.Collision.Bodies
{
    public sealed class CollisionBodyRegistry
    {
        private readonly List<CollisionBody> _bodies = new();
        
        public IReadOnlyList<CollisionBody> Bodies => _bodies;

        public void Register(CollisionBody body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            
            if (_bodies.Contains(body))
                throw new InvalidOperationException("Collision body is already registered.");
            
            _bodies.Add(body);
        }

        public void Unregister(CollisionBody body)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            
            _bodies.Remove(body);
        }
    }
}