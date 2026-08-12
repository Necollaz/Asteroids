namespace AsteroidGame.Scripts.Gameplay.Asteroids.Contracts
{
    public interface IAsteroidView
    {
        void Show();
        
        void Hide();
        
        void SetPosition(float x, float y);
        
        void SetRotation(float rotationDegrees);
    }
}