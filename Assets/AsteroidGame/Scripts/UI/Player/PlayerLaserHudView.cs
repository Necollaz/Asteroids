using System;
using UnityEngine;
using TMPro;

namespace AsteroidGame.Scripts.UI.Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerLaserHudView : MonoBehaviour
    {
        private const string ReadyText = "Ready";

        [SerializeField] private Transform _root;
        [SerializeField] private TextMeshProUGUI _chargesText;
        [SerializeField] private TextMeshProUGUI _rechargeText;

        private void Awake()
        {
            ValidateRequiredReferences();
            Show();
        }

        private void OnValidate()
        {
            if (_root == null)
                _root = transform;

            if (_chargesText == null)
                Debug.LogError($"{nameof(PlayerLaserHudView)} on {name} requires charges text.", this);

            if (_rechargeText == null)
                Debug.LogError($"{nameof(PlayerLaserHudView)} on {name} requires recharge text.", this);
        }

        public void Show()
        {
            ValidateRequiredReferences();
            _root.gameObject.SetActive(true);
        }

        public void SetCharges(int charges, int maxCharges)
        {
            ValidateRequiredReferences();
            _chargesText.text = $"{charges}/{maxCharges}";
        }

        public void SetRechargeTenths(int remainingTenths)
        {
            ValidateRequiredReferences();

            if (remainingTenths <= 0)
            {
                _rechargeText.text = ReadyText;

                return;
            }

            int seconds = remainingTenths / 10;
            int tenths = remainingTenths % 10;

            _rechargeText.text = $"{seconds}.{tenths}s";
        }

        private void ValidateRequiredReferences()
        {
            if (_root == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserHudView)} requires root.");

            if (_chargesText == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserHudView)} requires charges text.");

            if (_rechargeText == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserHudView)} requires recharge text.");
        }
    }
}