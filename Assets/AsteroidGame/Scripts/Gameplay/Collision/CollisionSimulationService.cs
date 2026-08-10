using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision;
using AsteroidGame.Scripts.Domain.Physics;

namespace AsteroidGame.Scripts.Gameplay.Collision
{
    public sealed class CollisionSimulationService : ITickable
    {
        private readonly CollisionBodyRegistry _registry;
        private readonly CollisionCategoryPolicy _categoryPolicy;
        private readonly CircleCollisionDetector _collisionDetector;
        private readonly PlayerEnemyCollisionHandler _playerCollisionHandler;

        public CollisionSimulationService(
            CollisionBodyRegistry registry,
            CollisionCategoryPolicy categoryPolicy,
            CircleCollisionDetector collisionDetector,
            PlayerEnemyCollisionHandler playerCollisionHandler)
        {
            _registry = registry;
            _categoryPolicy = categoryPolicy;
            _collisionDetector = collisionDetector;
            _playerCollisionHandler = playerCollisionHandler;
        }

        void ITickable.Tick()
        {
            IReadOnlyList<CollisionBody> bodies = _registry.Bodies;

            for (int i = 0; i < bodies.Count; i++)
            {
                for (int j = 0; j < bodies.Count; j++)
                {
                    CollisionBody first = bodies[i];
                    CollisionBody second = bodies[j];
                    
                    if (!_categoryPolicy.ShouldCheck(first.Category, second.Category))
                        continue;
                    
                    if (!_collisionDetector.TryDetect(first, second, out CollisionContact contact))
                        continue;
                    
                    _playerCollisionHandler.Handle(contact);
                }
            }
        }
    }
}