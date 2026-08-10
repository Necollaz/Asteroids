namespace AsteroidGame.Scripts.Domain.Physics
{
    public sealed class PhysicsValueFactory
    {
        public Vector2D CreateVector(float x, float y) => new Vector2D(x, y);
        
        public Velocity CreateVelocity(Vector2D value) => new Velocity(value);
        
        public Acceleration CreateAcceleration(Vector2D value) => new Acceleration(value);
    }
}