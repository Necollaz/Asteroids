using UnityEngine;
using UnityEngine.EventSystems;

namespace AsteroidGame.Scripts.Input.Mobile.Views
{
    public sealed class MobileInputButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        private bool _isPressed;
        private bool _wasPressedThisFrame;
        
        public bool IsPressed => _isPressed;

        private void OnDisable() => _isPressed = false;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            _wasPressedThisFrame = true;
        }
        
        public void OnPointerUp(PointerEventData eventData) => _isPressed = false;
        
        public void OnPointerExit(PointerEventData eventData) => _isPressed = false;
        
        public bool ConsumePressedThisFrame()
        {
            if (!_wasPressedThisFrame)
                return false;

            _wasPressedThisFrame = false;
            
            return true;
        }

        private void ResetState()
        {
            _isPressed = false;
            _wasPressedThisFrame = false;
        }
    }
}