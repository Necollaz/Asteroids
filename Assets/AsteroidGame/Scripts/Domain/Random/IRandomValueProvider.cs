namespace AsteroidGame.Scripts.Domain.Random
{
    public interface IRandomValueProvider
    {
        int Range(int minInclusive, int maxExclusive);
        
        float Range(float minInclusive, float maxExclusive);
    }
}