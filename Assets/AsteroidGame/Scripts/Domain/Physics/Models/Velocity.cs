namespace AsteroidGame.Scripts.Domain.Physics.Models
{
    public readonly struct Velocity
    {
        public Velocity(Vector2D value) => Value = value;
        
        public Vector2D Value { get; }
    }
}