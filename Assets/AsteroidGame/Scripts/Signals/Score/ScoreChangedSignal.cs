namespace AsteroidGame.Scripts.Signals.Score
{
    public sealed class ScoreChangedSignal
    {
        public ScoreChangedSignal(int score) => Score = score;

        public int Score { get; }
    }
}