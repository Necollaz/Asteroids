namespace AsteroidGame.Scripts.Domain.Physics
{
    public sealed class Body2D
    {
        public Body2D(Vector2D position, Velocity velocity, float rotationDegrees)
        {
            Position = position;
            Velocity = velocity;
            RotationDegrees = rotationDegrees;
        }
        
        public Vector2D Position { get; private set; }
        public Velocity Velocity { get; private set; }
        public float RotationDegrees { get; private set; }
        
        public void SetPosition(Vector2D position) => Position = position;
        
        public void SetVelocity(Velocity velocity) => Velocity = velocity;
        
        public void SetRotation(float rotationDegrees) => RotationDegrees = rotationDegrees;
    }
}