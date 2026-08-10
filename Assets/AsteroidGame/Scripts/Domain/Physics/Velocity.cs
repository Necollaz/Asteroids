namespace AsteroidGame.Scripts.Domain.Physics
{
    public readonly struct Velocity
    {
        public Velocity(Vector2D value) => Value = value;
        
        public Vector2D Value { get; }
    }
}