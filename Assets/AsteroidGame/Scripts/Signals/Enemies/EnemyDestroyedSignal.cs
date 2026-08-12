using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Signals.Enemies
{
    public sealed class EnemyDestroyedSignal
    {
        public EnemyDestroyedSignal(EnemyType enemyType, Vector2D position)
        {
            EnemyType = enemyType;
            Position = position;
        }

        public EnemyType EnemyType { get; }
        public Vector2D Position { get; }
    }
}