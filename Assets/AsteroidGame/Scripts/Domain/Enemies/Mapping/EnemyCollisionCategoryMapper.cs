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
    }
}