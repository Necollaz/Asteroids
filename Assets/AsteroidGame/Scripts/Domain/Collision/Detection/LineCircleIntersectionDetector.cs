using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Domain.Collision.Detection
{
    public sealed class LineCircleIntersectionDetector
    {
        public bool IntersectsSegmentCircle(Vector2D start, Vector2D end, Vector2D circleCenter, float radius)
        {
            Vector2D segment = end.Subtract(start);
            Vector2D startToCenter = circleCenter.Subtract(start);
            float segmentSqrLength = segment.SqrMagnitude;
            
            if (segmentSqrLength <= float.Epsilon)
                return false;
            
            float projection = Dot(startToCenter, segment) / segmentSqrLength;
            float clampedProjection = Clamp01(projection);
            Vector2D closesPoint = start.Add(segment.Multiply(clampedProjection));
            Vector2D closestToCenter = circleCenter.Subtract(closesPoint);
            
            return closestToCenter.SqrMagnitude <= radius * radius;
        }
        
        private float Dot(Vector2D first, Vector2D second) => first.X * second.X + first.Y * second.Y;

        private float Clamp01(float value)
        {
            if (value < 0f)
                return 0f;
            
            if (value > 1f)
                return 1f;
            
            return value;
        }
    }
}