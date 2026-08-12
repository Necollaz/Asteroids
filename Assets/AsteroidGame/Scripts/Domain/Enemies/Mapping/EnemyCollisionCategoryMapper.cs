using System;
using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Enemies.Types;

namespace AsteroidGame.Scripts.Domain.Enemies.Mapping
{
    public sealed class EnemyCollisionCategoryMapper
    {
        public CollisionCategory ToCollisionCategory(EnemyType enemyType)
        {
            return enemyType switch
            {
                EnemyType.LargeAsteroid => CollisionCategory.LargeAsteroid,
                EnemyType.MediumAsteroid => CollisionCategory.MediumAsteroid,
                EnemyType.SmallAsteroid => CollisionCategory.SmallAsteroid,
                EnemyType.Ufo => CollisionCategory.Ufo,
                _ => throw new ArgumentOutOfRangeException(nameof(EnemyType), enemyType, null)
            };
        }

        public bool TryGetEnemyType(CollisionCategory category, out EnemyType enemyType)
        {
            enemyType = category switch
            {
                CollisionCategory.LargeAsteroid => EnemyType.LargeAsteroid,
                CollisionCategory.MediumAsteroid => EnemyType.MediumAsteroid,
                CollisionCategory.SmallAsteroid => EnemyType.SmallAsteroid,
                CollisionCategory.Ufo => EnemyType.Ufo,
                _ => default
            };
            
            return category == CollisionCategory.LargeAsteroid ||
                   category == CollisionCategory.MediumAsteroid ||
                   category == CollisionCategory.SmallAsteroid ||
                   category == CollisionCategory.Ufo;
        }
    }
}