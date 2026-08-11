using System;
using AsteroidGame.Scripts.Domain.Collision.Bodies;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Models
{
    public readonly struct BulletEnemyCollisionContact
    {
        public BulletEnemyCollisionContact(CollisionBody bulletBody, CollisionBody enemtBody)
        {
            BulletBody = bulletBody ?? throw new ArgumentNullException(nameof(bulletBody));
            EnemyBody = enemtBody ?? throw new ArgumentNullException(nameof(enemtBody));
        }
        
        public CollisionBody BulletBody { get; }
        public CollisionBody EnemyBody { get; }
    }
}