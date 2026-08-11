namespace AsteroidGame.Scripts.Gameplay.Bullets.Contracts
{
    public interface IBulletView
    {
        void Show();
        
        void Hide();
        
        void SetPosition(float x, float y);
        
        void SetRotation(float rotationDegrees);
    }
}