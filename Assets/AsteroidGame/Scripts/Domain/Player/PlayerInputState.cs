namespace AsteroidGame.Scripts.Domain.Player
{
    public readonly struct PlayerInputState
    {
        public PlayerInputState(float turnDirection, bool isThrustPressed)
        {
            if (turnDirection > 1f)
                turnDirection = 1f;
            
            if (turnDirection < -1f)
                turnDirection = -1f;

            TurnDirection = turnDirection;
            IsThrustPressed  = isThrustPressed;
        }
        
        public float TurnDirection { get; }
        public bool IsThrustPressed  { get; }
    }
}