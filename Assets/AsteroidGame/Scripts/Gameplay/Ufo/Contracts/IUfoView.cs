namespace AsteroidGame.Scripts.Gameplay.Ufo.Contracts
{
    public interface IUfoView
    {
        void Show();
        
        void Hide();
        
        void SetPosition(float x, float y);
        
        void SetRotation(float rotationDegrees);
    }
}