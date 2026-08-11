using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Services
{
    public sealed class BulletEnemyCollisionContactResolver
    {
        private readonly CollisionCategoryPolicy _categoryPolicy;

        public BulletEnemyCollisionContactResolver(CollisionCategoryPolicy categoryPolicy) =>
            _categoryPolicy = categoryPolicy;

        public bool TryResolve(CollisionContact contact, out BulletEnemyCollisionContact bulletEnemyContact)
        {
            bulletEnemyContact = default;
            
            CollisionBody bulletBody = GetBulletBody(contact);
            CollisionBody enemyBody = GetEnemtBody(contact);
            
            if (bulletBody == null || enemyBody == null)
                return false;
            
            bulletEnemyContact = new BulletEnemyCollisionContact(bulletBody, enemyBody);
            
            return true;
        }

        private CollisionBody GetBulletBody(CollisionContact contact)
        {
            if (_categoryPolicy.IsBullet(contact.First.Category))
                return contact.First;
            
            if (_categoryPolicy.IsBullet(contact.Second.Category))
                return contact.Second;
            
            return null;
        }

        private CollisionBody GetEnemtBody(CollisionContact contact)
        {
            if (_categoryPolicy.IsEnemy(contact.First.Category))
                return contact.First;
            
            if (_categoryPolicy.IsEnemy(contact.Second.Category))
                return contact.Second;
            
            return null;
        }
    }
}