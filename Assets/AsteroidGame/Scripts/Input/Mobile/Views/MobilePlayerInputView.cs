using System;
using UnityEngine;

namespace AsteroidGame.Scripts.Input.Mobile.Views
{
    [DisallowMultipleComponent]
    public sealed class MobilePlayerInputView : MonoBehaviour
    {
        [SerializeField] private RectTransform _root;
        [SerializeField] private MobileInputButtonView _turnLeftButton;
        [SerializeField] private MobileInputButtonView _turnRightButton;
        [SerializeField] private MobileInputButtonView _moveButton;
        [SerializeField] private MobileInputButtonView _fireButton;
        [SerializeField] private MobileInputButtonView _laserButton;
        
        public bool IsTurnLeftPressed => _turnLeftButton.IsPressed;
        public bool IsTurnRightPressed => _turnRightButton.IsPressed;
        public bool IsMovePressed => _moveButton.IsPressed;
        public bool IsFirePressed => _fireButton.IsPressed;

        private void Awake()
        {
            ValidateRequiredReferences();
            Hide();
        }

        private void OnValidate()
        {
            if (_root == null)
                TryGetComponent(out _root);
        }
        
        public bool ConsumeLaserFirePressedThisFrame() => _laserButton.ConsumePressedThisFrame();

        public void Show()
        {
            ValidateRequiredReferences();
            _root.gameObject.SetActive(true);
        }

        public void Hide()
        {
            ValidateRequiredReferences();
            _root.gameObject.SetActive(false);
        }

        private void ValidateRequiredReferences()
        {
            if (_root == null)
                throw new InvalidOperationException($"{nameof(MobilePlayerInputView)} requires root.");

            if (_turnLeftButton == null)
                throw new InvalidOperationException($"{nameof(MobilePlayerInputView)} requires TurnLeftButton.");

            if (_turnRightButton == null)
                throw new InvalidOperationException($"{nameof(MobilePlayerInputView)} requires TurnRightButton.");

            if (_moveButton == null)
                throw new InvalidOperationException($"{nameof(MobilePlayerInputView)} requires MoveButton.");

            if (_fireButton == null)
                throw new InvalidOperationException($"{nameof(MobilePlayerInputView)} requires FireButton.");
            
            if (_laserButton == null)
                throw new InvalidOperationException($"{nameof(MobilePlayerInputView)} requires LaserButton.");
        }
    }
}