namespace AsteroidGame.Scripts.Domain.Score
{
    public sealed class ScoreState
    {
        public int Value { get; private set; }

        public void Add(int amount) => Value += amount;

        public void Reset() => Value = 0;
    }
}