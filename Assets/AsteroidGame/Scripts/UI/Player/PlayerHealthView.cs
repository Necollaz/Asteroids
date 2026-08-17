using System;
using UnityEngine;

namespace AsteroidGame.Scripts.UI.Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private PlayerHealthIconView[] _healthIcons;

        private void Awake() => ValidateRequiredReferences();

        private void OnValidate()
        {
            if (_root == null)
                Debug.LogError($"{nameof(PlayerHealthView)} on {name} requires root.", this);

            if (_healthIcons == null || _healthIcons.Length == 0)
                Debug.LogError($"{nameof(PlayerHealthView)} on {name} requires health icons.", this);
        }

        public void Render(PlayerHealthViewModel viewModel)
        {
            ValidateRequiredReferences();

            if (viewModel.FilledIcons.Length > _healthIcons.Length)
                throw new InvalidOperationException($"{nameof(PlayerHealthView)} has not enough health icons.");

            _root.gameObject.SetActive(viewModel.IsVisible);

            for (int i = 0; i < _healthIcons.Length; i++)
            {
                bool isFilled = i < viewModel.FilledIcons.Length && viewModel.FilledIcons[i];
                _healthIcons[i].SetFilled(isFilled);
            }
        }

        private void ValidateRequiredReferences()
        {
            if (_root == null)
                throw new InvalidOperationException($"{nameof(PlayerHealthView)} requires root.");

            if (_healthIcons == null || _healthIcons.Length == 0)
                throw new InvalidOperationException($"{nameof(PlayerHealthView)} requires health icons.");

            for (int i = 0; i < _healthIcons.Length; i++)
                _healthIcons[i].ValidateRequiredReferences();
        }
    }
}