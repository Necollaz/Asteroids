using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Gameplay.Bullets.Services;

namespace AsteroidGame.Scripts.Gameplay.Collision
{
    public sealed class CollisionContactRouter
    {
        private readonly PlayerEnemyCollisionHandler _playerEnemyCollisionHandler;
        private readonly BulletEnemyCollisionHandler _bulletEnemyCollisionHandler;

        public CollisionContactRouter(
            PlayerEnemyCollisionHandler playerEnemyCollisionHandler,
            BulletEnemyCollisionHandler bulletEnemyCollisionHandler)
        {
            _playerEnemyCollisionHandler = playerEnemyCollisionHandler;
            _bulletEnemyCollisionHandler = bulletEnemyCollisionHandler;
        }

        public void Handle(CollisionContact contact)
        {
            _playerEnemyCollisionHandler.Handle(contact);
            _bulletEnemyCollisionHandler.Handle(contact);
        }
    }
}