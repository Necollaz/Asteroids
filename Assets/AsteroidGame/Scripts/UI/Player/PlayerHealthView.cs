using System;
using UnityEngine;

namespace AsteroidGame.Scripts.UI.Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerHealthView : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private PlayerHealthIconView[] _healthIcons;
        [SerializeField] private float _initialVisibleSeconds = 7f;
        [SerializeField] private float _damageVisibleSeconds = 2f;

        public float InitialVisibleSeconds => _initialVisibleSeconds;
        public float DamageVisibleSeconds => _damageVisibleSeconds;

        private void Awake()
        {
            ValidateRequiredReferences();
            Show();
        }

        private void OnValidate()
        {
            if (_root == null)
                Debug.LogError($"{nameof(PlayerHealthView)} on {name} requires root.", this);

            if (_healthIcons == null || _healthIcons.Length == 0)
                Debug.LogError($"{nameof(PlayerHealthView)} on {name} requires health icons.", this);

            if (_initialVisibleSeconds <= 0f)
            {
                Debug.LogError($"{nameof(PlayerHealthView)} on {name} requires positive initial visible seconds.",
                    this);
            }

            if (_damageVisibleSeconds <= 0f)
            {
                Debug.LogError($"{nameof(PlayerHealthView)} on {name} requires positive damage visible seconds.", 
                    this);
            }
        }

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

        public void SetHealth(int currentHealth, int maxHealth)
        {
            ValidateRequiredReferences();

            if (maxHealth > _healthIcons.Length)
                throw new InvalidOperationException($"{nameof(PlayerHealthView)} has not enough health icons.");

            for (int i = 0; i < _healthIcons.Length; i++)
                _healthIcons[i].SetFilled(i < currentHealth);
        }

        private void ValidateRequiredReferences()
        {
            if (_root == null)
                throw new InvalidOperationException($"{nameof(PlayerHealthView)} requires root.");

            if (_healthIcons == null || _healthIcons.Length == 0)
                throw new InvalidOperationException($"{nameof(PlayerHealthView)} requires health icons.");

            if (_initialVisibleSeconds <= 0f)
            {
                throw new InvalidOperationException(
                    $"{nameof(PlayerHealthView)} requires positive initial visible seconds.");
            }

            if (_damageVisibleSeconds <= 0f)
            {
                throw new InvalidOperationException(
                    $"{nameof(PlayerHealthView)} requires positive damage visible seconds.");
            }

            for (int i = 0; i < _healthIcons.Length; i++)
                _healthIcons[i].ValidateRequiredReferences();
        }
    }
}