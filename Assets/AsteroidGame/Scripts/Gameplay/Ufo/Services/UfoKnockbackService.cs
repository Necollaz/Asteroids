using System;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Ufo.Settings;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Pooling;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Services
{
    public sealed class UfoKnockbackService
    {
        private readonly UfoPool _pool;
        private readonly UfoSettings _settings;

        public UfoKnockbackService(UfoPool pool, UfoSettings settings)
        {
            _pool = pool;
            _settings = settings;
        }

        public void ApplyIfUfo(CollisionBody enemyBody)
        {
            if (enemyBody == null)
                throw new ArgumentNullException(nameof(enemyBody));
            
            if (enemyBody.Category != CollisionCategory.Ufo)
                return;
            
            if (!_pool.TryGetByCollisionBody(enemyBody, out UfoInstance ufo))
            {
                throw new InvalidOperationException(
                    "UFO collision body was found, but active UFO instance was not found.");
            }

            ufo.KnockbackState.Activate(_settings.KnockbackSeconds);
        }
    }
}