using System.Collections.Generic;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Detection;
using AsteroidGame.Scripts.Domain.Collision.Rules;
using AsteroidGame.Scripts.Gameplay.Game;

namespace AsteroidGame.Scripts.Gameplay.Collision
{
    public sealed class CollisionSimulationService : IFixedTickable
    {
        private readonly CollisionBodyRegistry _registry;
        private readonly CollisionCategoryPolicy _categoryPolicy;
        private readonly CircleCollisionDetector _collisionDetector;
        private readonly CollisionContactRouter _contactRouter;
        private readonly IGameplayPauseState _pauseState;

        private readonly List<CollisionBody> _activeBodies = new();
        private readonly List<CollisionContact> _contacts = new();

        public CollisionSimulationService(
            CollisionBodyRegistry registry,
            CollisionCategoryPolicy categoryPolicy,
            CircleCollisionDetector collisionDetector,
            CollisionContactRouter contactRouter,
            IGameplayPauseState pauseState)
        {
            _registry = registry;
            _categoryPolicy = categoryPolicy;
            _collisionDetector = collisionDetector;
            _contactRouter = contactRouter;
            _pauseState = pauseState;
        }

        void IFixedTickable.FixedTick()
        {
            if (_pauseState.IsPaused)
                return;

            CollectActiveBodies();
            CollectContacts();
            HandleContacts();
        }

        private void CollectActiveBodies()
        {
            _activeBodies.Clear();
            IReadOnlyList<CollisionBody> bodies = _registry.Bodies;

            for (int i = 0; i < bodies.Count; i++)
            {
                CollisionBody body = bodies[i];
                
                if (body.IsActive)
                    _activeBodies.Add(body);
            }
        }
        
        private void CollectContacts()
        {
            _contacts.Clear();

            for (int i = 0; i < _activeBodies.Count; i++)
            {
                for (int j = i + 1; j < _activeBodies.Count; j++)
                {
                    CollisionBody first = _activeBodies[i];
                    CollisionBody second = _activeBodies[j];

                    if (!_categoryPolicy.ShouldCheck(first.Category, second.Category))
                        continue;

                    if (_collisionDetector.TryDetect(first, second, out CollisionContact contact))
                        _contacts.Add(contact);
                }
            }
        }

        private void HandleContacts()
        {
            for (int i = 0; i < _contacts.Count; i++)
                _contactRouter.Handle(_contacts[i]);
        }
    }
}