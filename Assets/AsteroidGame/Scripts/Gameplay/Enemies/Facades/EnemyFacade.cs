using System;
using System.Collections.Generic;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Gameplay.Asteroids.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Pooling;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Pooling;

namespace AsteroidGame.Scripts.Gameplay.Enemies.Facades
{
    public sealed class EnemyFacade
    {
        private readonly AsteroidPool _asteroidPool;
        private readonly UfoPool _ufoPool;

        public EnemyFacade(AsteroidPool asteroidPool, UfoPool ufoPool)
        {
            _asteroidPool = asteroidPool ?? throw new ArgumentNullException(nameof(asteroidPool));
            _ufoPool = ufoPool ?? throw new ArgumentNullException(nameof(ufoPool));
        }

        public bool ContainsActiveEnemyBody(CollisionBody body)
        {
            if (body == null)
                return false;

            return TryGetEnemyType(body, out _);
        }

        public void CollectActiveEnemyBodies(List<CollisionBody> buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));

            buffer.Clear();
            AddActiveAsteroidBodies(buffer);
            AddActiveUfoBodies(buffer);
        }

        private bool TryGetAsteroidType(CollisionBody body, out EnemyType enemyType)
        {
            IReadOnlyList<AsteroidInstance> asteroids = _asteroidPool.ActiveAsteroids;

            for (int i = 0; i < asteroids.Count; i++)
            {
                AsteroidInstance asteroid = asteroids[i];

                if (!ReferenceEquals(asteroid.CollisionBody, body))
                    continue;

                enemyType = asteroid.Type;
                
                return true;
            }

            enemyType = default;
            
            return false;
        }
        
        private bool TryGetEnemyType(CollisionBody body, out EnemyType enemyType)
        {
            enemyType = default;

            if (body == null)
                return false;

            if (TryGetAsteroidType(body, out enemyType))
                return true;

            return TryGetUfoType(body, out enemyType);
        }

        private bool TryGetUfoType(CollisionBody body, out EnemyType enemyType)
        {
            IReadOnlyList<UfoInstance> ufos = _ufoPool.ActiveUfos;

            for (int i = 0; i < ufos.Count; i++)
            {
                UfoInstance ufo = ufos[i];

                if (!ReferenceEquals(ufo.CollisionBody, body))
                    continue;

                enemyType = ufo.Type;
                
                return true;
            }

            enemyType = default;
            
            return false;
        }

        private void AddActiveAsteroidBodies(List<CollisionBody> buffer)
        {
            IReadOnlyList<AsteroidInstance> asteroids = _asteroidPool.ActiveAsteroids;

            for (int i = 0; i < asteroids.Count; i++)
            {
                CollisionBody body = asteroids[i].CollisionBody;

                if (body.IsActive)
                    buffer.Add(body);
            }
        }

        private void AddActiveUfoBodies(List<CollisionBody> buffer)
        {
            IReadOnlyList<UfoInstance> ufos = _ufoPool.ActiveUfos;

            for (int i = 0; i < ufos.Count; i++)
            {
                CollisionBody body = ufos[i].CollisionBody;

                if (body.IsActive)
                    buffer.Add(body);
            }
        }
    }
}