using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Models;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Factories
{
    public readonly struct EnemyInstanceContext
    {
        public EnemyInstanceContext(EnemyModel enemy, CollisionBody collisionBody)
        {
            Enemy = enemy;
            CollisionBody = collisionBody;
        }
        
        public EnemyModel Enemy { get; }
        public CollisionBody CollisionBody { get; }
    }
}