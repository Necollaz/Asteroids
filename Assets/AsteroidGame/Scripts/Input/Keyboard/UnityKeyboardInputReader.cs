using UnityEngine;
using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Input.Keyboard
{
    public sealed class UnityKeyboardInputReader : IKeyboardInputReader
    {
        public bool IsHeld(KeyCode keyCode) => UnityEngine.Input.GetKey(keyCode);

        public bool IsPressedThisFrame(KeyCode keyCode) => UnityEngine.Input.GetKeyDown(keyCode);
    }
}