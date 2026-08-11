using System;
using AsteroidGame.Scripts.Domain.Bullets.Settings;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Bullets.Models
{
    public sealed class BulletModel
    {
        private readonly BulletSettings _settings;

        private float _ageSeconds;

        public BulletModel(Body2D body, BulletSettings settings)
        {
            Body = body ?? throw new ArgumentNullException(nameof(body));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        } 
        
        public Body2D Body { get; }
        public bool IsActive { get; private set; }
        
        public BulletSnapshot CreateSnapshot() => new BulletSnapshot(Body.Position, Body.RotationDegrees);

        public bool TickLifetime(float deltaTime)
        {
            if (!IsActive)
                return false;
            
            _ageSeconds += deltaTime;
            
            return _ageSeconds >= _settings.LifetimeSeconds;
        }
        
        public void Activate(Vector2D position, Velocity velocity, float rotationDegrees)
        {
            Body.SetPosition(position);
            Body.SetVelocity(velocity);
            Body.SetRotation(rotationDegrees);

            _ageSeconds = 0f;
            IsActive = true;
        }

        public void Deactivate()
        {
            _ageSeconds = 0f;
            IsActive = false;
        }
    }
}