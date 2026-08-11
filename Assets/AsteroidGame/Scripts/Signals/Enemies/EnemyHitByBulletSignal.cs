using System;
using AsteroidGame.Scripts.Domain.Collision.Bodies;

namespace AsteroidGame.Scripts.Signals.Enemies
{
    public sealed class EnemyHitByBulletSignal
    {
        public EnemyHitByBulletSignal(CollisionBody enemyBody) =>
            EnemyBody = enemyBody ?? throw new ArgumentNullException(nameof(enemyBody));
        
        public CollisionBody EnemyBody  { get; }
    }
}