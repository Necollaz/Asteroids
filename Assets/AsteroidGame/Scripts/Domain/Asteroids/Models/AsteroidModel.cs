using System;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Asteroids.Models
{
    public sealed class AsteroidModel
    {
        public AsteroidModel(EnemyModel enemy) => Enemy = enemy ?? throw new ArgumentNullException(nameof(enemy));
        
        public EnemyModel Enemy { get; }
        public Body2D Body => Enemy.Body;
        public EnemyType Type => Enemy.Type;
        public bool IsActive => Enemy.IsActive;
        
        public void Activate(Vector2D position, Velocity velocity, float rotationDegrees) =>
            Enemy.Activate(position, velocity, rotationDegrees);
        
        public void Deactivate() => Enemy.Deactivate();
    }
}