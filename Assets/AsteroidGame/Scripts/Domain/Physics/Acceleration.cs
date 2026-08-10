namespace AsteroidGame.Scripts.Domain.Physics
{
    public readonly struct Acceleration
    {
        public Acceleration(Vector2D value) => Value = value;
        
        public Vector2D Value { get; }
    }
}