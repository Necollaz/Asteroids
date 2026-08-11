namespace AsteroidGame.Scripts.Domain.Player.Models
{
    public readonly struct PlayerInputState
    {
        public PlayerInputState(
            float turnDirection,
            bool isThrustPressed,
            bool isBulletFirePressed,
            bool isLaserFirePressed)
        {
            if (turnDirection > 1f)
                turnDirection = 1f;
            
            if (turnDirection < -1f)
                turnDirection = -1f;

            TurnDirection = turnDirection;
            IsThrustPressed  = isThrustPressed;
            IsBulletFirePressed = isBulletFirePressed;
            IsLaserFirePressed = isLaserFirePressed;
        }
        
        public float TurnDirection { get; }
        public bool IsThrustPressed { get; }
        public bool IsBulletFirePressed { get; }
        public bool IsLaserFirePressed { get; }
    }
}