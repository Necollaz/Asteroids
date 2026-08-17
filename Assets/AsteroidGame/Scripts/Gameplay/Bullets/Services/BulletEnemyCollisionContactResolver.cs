using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;
using AsteroidGame.Scripts.Gameplay.Bullets.Models;
using AsteroidGame.Scripts.Gameplay.Enemies.Facades;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Services
{
    public sealed class BulletEnemyCollisionContactResolver
    {
        private readonly CollisionCategoryPolicy _categoryPolicy;
        private readonly EnemyFacade _enemyFacade;

        public BulletEnemyCollisionContactResolver(
            CollisionCategoryPolicy categoryPolicy,
            EnemyFacade enemyFacade)
        {
            _categoryPolicy = categoryPolicy;
            _enemyFacade = enemyFacade;
        }

        public bool TryResolve(CollisionContact contact, out BulletEnemyCollisionContact bulletEnemyContact)
        {
            bulletEnemyContact = default;

            CollisionBody bulletBody = GetBulletBody(contact);
            CollisionBody enemyBody = GetEnemyBody(contact);

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

        private CollisionBody GetEnemyBody(CollisionContact contact)
        {
            if (_enemyFacade.ContainsActiveEnemyBody(contact.First))
                return contact.First;

            if (_enemyFacade.ContainsActiveEnemyBody(contact.Second))
                return contact.Second;

            return null;
        }
    }
}