using UnityEngine;

namespace AsteroidGame.Scripts.Input.Contracts
{
    public interface IKeyboardInputReader
    {
        bool IsHeld(KeyCode keyCode);
        bool IsPressedThisFrame(KeyCode keyCode);
    }
}