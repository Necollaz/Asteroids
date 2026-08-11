using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;

namespace AsteroidGame.Scripts.Gameplay.Collision
{
    public sealed class PlayerEnemyCollisionContactResolver
    {
        private readonly CollisionCategoryPolicy _categoryPolicy;

        public PlayerEnemyCollisionContactResolver(CollisionCategoryPolicy categoryPolicy) => 
            _categoryPolicy = categoryPolicy;

        public bool TryResolve(CollisionContact contact, out PlayerEnemyCollisionContact playerEnemyContact)
        {
            playerEnemyContact = default;

            CollisionBody playerBody = GetPlayerBody(contact);
            CollisionBody enemyBody = GetEnemyBody(contact);

            if (playerBody == null || enemyBody == null)
                return false;

            playerEnemyContact = new PlayerEnemyCollisionContact(playerBody, enemyBody);

            return true;
        }

        private CollisionBody GetPlayerBody(CollisionContact contact)
        {
            if (_categoryPolicy.IsPlayer(contact.First.Category))
                return contact.First;

            if (_categoryPolicy.IsPlayer(contact.Second.Category))
                return contact.Second;

            return null;
        }

        private CollisionBody GetEnemyBody(CollisionContact contact)
        {
            if (_categoryPolicy.IsEnemy(contact.First.Category))
                return contact.First;

            if (_categoryPolicy.IsEnemy(contact.Second.Category))
                return contact.Second;

            return null;
        }
    }
}