using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Enemies.Models;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Factories;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Factories
{
    public sealed class EnemyInstanceContextFactory
    {
        private readonly PhysicsValueFactory _physicsValueFactory;
        private readonly Body2DFactory _bodyFactory;
        private readonly EnemyModelFactory _enemyModelFactory;
        private readonly CollisionBodyFactory _collisionBodyFactory;

        public EnemyInstanceContextFactory(
            PhysicsValueFactory physicsValueFactory,
            Body2DFactory bodyFactory,
            EnemyModelFactory enemyModelFactory,
            CollisionBodyFactory collisionBodyFactory)
        {
            _physicsValueFactory = physicsValueFactory;
            _bodyFactory = bodyFactory;
            _enemyModelFactory = enemyModelFactory;
            _collisionBodyFactory = collisionBodyFactory;
        }

        public EnemyInstanceContext Create(EnemyType type, CollisionCategory category, float radius)
        {
            Vector2D position = _physicsValueFactory.CreateVector(0f, 0f);
            Velocity velocity = _physicsValueFactory.CreateVelocity(position);
            Body2D body = _bodyFactory.Create(position, velocity, 0f);
            EnemyModel enemy = _enemyModelFactory.Create(type, body);
            CollisionBody collisionBody = _collisionBodyFactory.Create(category, body, radius);
            
            return new EnemyInstanceContext(enemy, collisionBody);
        }
    }
}