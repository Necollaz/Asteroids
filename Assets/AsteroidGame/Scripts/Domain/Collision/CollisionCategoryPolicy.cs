namespace AsteroidGame.Scripts.Domain.Collision
{
    public sealed class CollisionCategoryPolicy
    {
        public bool ShouldCheck(CollisionCategory first, CollisionCategory second)
        {
            if (IsEnemy(first) && IsEnemy(second))
                return false;

            if (IsPlayer(first) && IsEnemy(second))
                return true;

            if (IsEnemy(first) && IsPlayer(second))
                return true;

            if (IsBullet(first) && IsEnemy(second))
                return true;

            if (IsEnemy(first) && IsBullet(second))
                return true;

            if (IsLaser(first) && IsEnemy(second))
                return true;

            if (IsEnemy(first) && IsLaser(second))
                return true;
            
            return false;
        }

        public bool IsPlayer(CollisionCategory category) => category == CollisionCategory.Player;

        public bool IsEnemy(CollisionCategory category) => category == CollisionCategory.LargeAsteroid ||
                                                           category == CollisionCategory.MediumAsteroid ||
                                                           category == CollisionCategory.SmallAsteroid ||
                                                           category == CollisionCategory.Ufo;
        
        private bool IsBullet(CollisionCategory category) => category == CollisionCategory.Bullet;
        
        private bool IsLaser(CollisionCategory category) => category == CollisionCategory.Laser;
    }
}