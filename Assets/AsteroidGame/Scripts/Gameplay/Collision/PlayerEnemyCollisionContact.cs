using System;
using AsteroidGame.Scripts.Domain.Collision.Bodies;

namespace AsteroidGame.Scripts.Gameplay.Collision
{
    public readonly struct PlayerEnemyCollisionContact
    {
        public PlayerEnemyCollisionContact(CollisionBody playerBody, CollisionBody enemyBody)
        {
            PlayerBody = playerBody ?? throw new ArgumentNullException(nameof(playerBody));
            EnemyBody = enemyBody ?? throw new ArgumentNullException(nameof(enemyBody));
        }

        public CollisionBody PlayerBody { get; }
        public CollisionBody EnemyBody { get; }
    }
}