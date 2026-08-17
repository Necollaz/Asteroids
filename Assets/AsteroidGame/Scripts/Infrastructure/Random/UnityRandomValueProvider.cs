using AsteroidGame.Scripts.Domain.Random;

namespace AsteroidGame.Scripts.Infrastructure.Random
{
    public sealed class UnityRandomValueProvider: IRandomValueProvider
    {
        public int Range(int minInclusive, int maxExclusive) => UnityEngine.Random.Range(minInclusive, maxExclusive);

        public float Range(float minInclusive, float maxInclusive) =>
            UnityEngine.Random.Range(minInclusive, maxInclusive);
    }
}