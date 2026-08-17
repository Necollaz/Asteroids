using System;
using UnityEngine;
using TMPro;

namespace AsteroidGame.Scripts.UI.Player.Stats
{
    [DisallowMultipleComponent]
    public sealed class PlayerStatsHudView : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private TextMeshProUGUI _positionText;
        [SerializeField] private TextMeshProUGUI _rotationText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _laserChargesText;
        [SerializeField] private TextMeshProUGUI _laserCooldownText;

        private void Awake() => ValidateRequiredReferences();

        private void OnValidate()
        {
            if (_root == null)
                _root = transform;

            if (_positionText == null)
                Debug.LogError($"{nameof(PlayerStatsHudView)} on {name} requires position text.", this);

            if (_rotationText == null)
                Debug.LogError($"{nameof(PlayerStatsHudView)} on {name} requires rotation text.", this);

            if (_speedText == null)
                Debug.LogError($"{nameof(PlayerStatsHudView)} on {name} requires speed text.", this);

            if (_laserChargesText == null)
                Debug.LogError($"{nameof(PlayerStatsHudView)} on {name} requires laser charges text.", this);

            if (_laserCooldownText == null)
                Debug.LogError($"{nameof(PlayerStatsHudView)} on {name} requires laser cooldown text.", this);
        }

        public void Render(PlayerStatsHudViewModel viewModel)
        {
            ValidateRequiredReferences();
            _root.gameObject.SetActive(viewModel.IsVisible);

            if (!viewModel.IsVisible)
                return;

            _positionText.text = viewModel.PositionText;
            _rotationText.text = viewModel.RotationText;
            _speedText.text = viewModel.SpeedText;
            _laserChargesText.text = viewModel.LaserChargesText;
            _laserCooldownText.text = viewModel.LaserCooldownText;
        }

        private void ValidateRequiredReferences()
        {
            if (_root == null)
                throw new InvalidOperationException($"{nameof(PlayerStatsHudView)} requires root.");

            if (_positionText == null)
                throw new InvalidOperationException($"{nameof(PlayerStatsHudView)} requires position text.");

            if (_rotationText == null)
                throw new InvalidOperationException($"{nameof(PlayerStatsHudView)} requires rotation text.");

            if (_speedText == null)
                throw new InvalidOperationException($"{nameof(PlayerStatsHudView)} requires speed text.");

            if (_laserChargesText == null)
                throw new InvalidOperationException($"{nameof(PlayerStatsHudView)} requires laser charges text.");

            if (_laserCooldownText == null)
                throw new InvalidOperationException($"{nameof(PlayerStatsHudView)} requires laser cooldown text.");
        }
    }
}