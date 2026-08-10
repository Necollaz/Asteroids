using System;

namespace AsteroidGame.Scripts.Domain.Physics
{
    public readonly struct Vector2D
    {
        public Vector2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float SqrMagnitude => X * X + Y * Y;
        public float Magnitude => (float)Math.Sqrt(SqrMagnitude);
        public float X { get; }
        public float Y { get; }

        public Vector2D Normalized
        {
            get
            {
                float magnitude = Magnitude;

                if (magnitude <= float.Epsilon)
                    return new Vector2D(0f, 0f);

                return Divide(magnitude);
            }
        }

        public Vector2D Add(Vector2D other) => new Vector2D(X + other.X, Y + other.Y);

        public Vector2D Subtract(Vector2D other) => new Vector2D(X - other.X, Y - other.Y);

        public Vector2D Multiply(float multiplier) => new Vector2D(X * multiplier, Y * multiplier);

        public Vector2D Divide(float divider) => new Vector2D(X / divider, Y / divider);

        public Vector2D ClampMagnitude(float maxMagnitude)
        {
            if (maxMagnitude <= 0f)
                return new Vector2D(0f, 0f);

            float maxSqrMagnitude = maxMagnitude * maxMagnitude;

            if (SqrMagnitude <= maxSqrMagnitude)
                return this;

            return Normalized.Multiply(maxMagnitude);
        }
    }
}