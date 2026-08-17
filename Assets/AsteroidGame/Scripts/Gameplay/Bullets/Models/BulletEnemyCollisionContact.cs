using System;
using AsteroidGame.Scripts.Domain.Collision.Bodies;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Models
{
    public readonly struct BulletEnemyCollisionContact
    {
        public BulletEnemyCollisionContact(CollisionBody bulletBody, CollisionBody enemyBody)
        {
            BulletBody = bulletBody ?? throw new ArgumentNullException(nameof(bulletBody));
            EnemyBody = enemyBody ?? throw new ArgumentNullException(nameof(enemyBody));
        }
        
        public CollisionBody BulletBody { get; }
        public CollisionBody EnemyBody { get; }
    }
}