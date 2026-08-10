using AsteroidGame.Scripts.Domain.Collision;
using AsteroidGame.Scripts.Domain.Physics;
using AsteroidGame.Scripts.Domain.Player;
using AsteroidGame.Scripts.Gameplay.Player.Services;

namespace AsteroidGame.Scripts.Gameplay.Collision
{
    public sealed class PlayerEnemyCollisionHandler
    {
        private readonly CollisionCategoryPolicy _categoryPolicy;
        private readonly CustomPhysicsWorld _physicsWorld;
        private readonly PlayerDamageService _playerDamageService;
        private readonly PlayerCollisionSettings _settings;

        public PlayerEnemyCollisionHandler(
            CollisionCategoryPolicy categoryPolicy,
            CustomPhysicsWorld physicsWorld,
            PlayerDamageService playerDamageService,
            PlayerCollisionSettings settings)
        {
            _categoryPolicy = categoryPolicy;
            _physicsWorld = physicsWorld;
            _playerDamageService = playerDamageService;
            _settings = settings;
        }

        public void Handle(CollisionContact contact)
        {
            CollisionBody playerBody = GetPlayerBody(contact);
            CollisionBody enemyBody = GetEnemyBody(contact);
            
            if (playerBody == null || enemyBody == null)
                return;
            
            if (!_playerDamageService.CanReceiveCollisionDamage)
                return;
            
            _physicsWorld.ApplyBounce(playerBody.Body, enemyBody.Body, _settings.CollisionBounceSpeed);
            _playerDamageService.ApplyCollisionDamage();
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