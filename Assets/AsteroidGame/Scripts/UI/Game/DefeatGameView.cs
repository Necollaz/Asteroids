using System;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidGame.Scripts.UI.Game
{
    [DisallowMultipleComponent]
    public sealed class DefeatGameView : MonoBehaviour
    {
        public event Action RestartClicked;
        
        [SerializeField] private Transform _root;
        [SerializeField] private Button _restartButton;
        
        private void Awake()
        {
            ValidateRequiredReferences();
            _restartButton.onClick.AddListener(HandleRestartClicked);
        }

        private void OnDestroy()
        {
            if (_restartButton != null)
                _restartButton.onClick.RemoveListener(HandleRestartClicked);
        }

        private void OnValidate()
        {
            if (_root == null)
                Debug.LogError($"{nameof(DefeatGameView)} on {name} requires root.", this);

            if (_restartButton == null)
                Debug.LogError($"{nameof(DefeatGameView)} on {name} requires restart button.", this);
        }

        public void Render(DefeatGameViewModel viewModel)
        {
            ValidateRequiredReferences();
            _root.gameObject.SetActive(viewModel.IsVisible);
        }

        private void HandleRestartClicked() => RestartClicked?.Invoke();

        private void ValidateRequiredReferences()
        {
            if (_root == null)
                throw new InvalidOperationException($"{nameof(DefeatGameView)} requires root.");

            if (_restartButton == null)
                throw new InvalidOperationException($"{nameof(DefeatGameView)} requires restart button.");
        }
    }
}