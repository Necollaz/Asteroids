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

        private string _currentPositionText = string.Empty;
        private string _currentRotationText = string.Empty;
        private string _currentSpeedText = string.Empty;
        private string _currentLaserChargesText = string.Empty;
        private string _currentLaserCooldownText = string.Empty;
        
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

            if (_root.gameObject.activeSelf != viewModel.IsVisible)
                _root.gameObject.SetActive(viewModel.IsVisible);

            if (!viewModel.IsVisible)
                return;

            SetText(_positionText, ref _currentPositionText, viewModel.PositionText);
            SetText(_rotationText, ref _currentRotationText, viewModel.RotationText);
            SetText(_speedText, ref _currentSpeedText, viewModel.SpeedText);
            SetText(_laserChargesText, ref _currentLaserChargesText, viewModel.LaserChargesText);
            SetText(_laserCooldownText, ref _currentLaserCooldownText, viewModel.LaserCooldownText);
        }

        private static void SetText(TextMeshProUGUI text, ref string currentValue, string nextValue)
        {
            if (currentValue == nextValue)
                return;

            currentValue = nextValue;
            text.text = nextValue;
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