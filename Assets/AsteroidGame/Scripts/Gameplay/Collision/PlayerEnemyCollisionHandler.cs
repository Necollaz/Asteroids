using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Physics.Services;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Gameplay.Player.Services;

namespace AsteroidGame.Scripts.Gameplay.Collision
{
    public sealed class PlayerEnemyCollisionHandler
    {
        private readonly PlayerEnemyCollisionContactResolver _contactResolver;
        private readonly CustomPhysicsWorld _physicsWorld;
        private readonly PlayerDamageService _playerDamageService;
        private readonly PlayerCollisionSettings _settings;

        public PlayerEnemyCollisionHandler(
            PlayerEnemyCollisionContactResolver contactResolver,
            CustomPhysicsWorld physicsWorld,
            PlayerDamageService playerDamageService,
            PlayerCollisionSettings settings)
        {
            _contactResolver = contactResolver;
            _physicsWorld = physicsWorld;
            _playerDamageService = playerDamageService;
            _settings = settings;
        }

        public void Handle(CollisionContact contact)
        {
            if (!_contactResolver.TryResolve(contact, out PlayerEnemyCollisionContact playerEnemyContact))
                return;

            if (!_playerDamageService.CanReceiveCollisionDamage)
                return;

            _physicsWorld.ApplyBounce(
                playerEnemyContact.PlayerBody.Body,
                playerEnemyContact.EnemyBody.Body,
                _settings.CollisionBounceSpeed);
            _playerDamageService.ApplyCollisionDamage();
        }
    }
}