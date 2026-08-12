using System;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Enemies.Models
{
    public sealed class EnemyModel
    {
        public EnemyModel(EnemyType type, Body2D body)
        {
            Type = type;
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }
        
        public EnemyType Type { get; }
        public Body2D Body { get; }
        public bool IsActive {get; private set;}

        public void Activate(Vector2D position, Velocity velocity, float rotationDegrees)
        {
            Body.SetPosition(position);
            Body.SetVelocity(velocity);
            Body.SetRotation(rotationDegrees);
            IsActive = true;
        }
        
        public void Deactivate() => IsActive = false;
    }
}