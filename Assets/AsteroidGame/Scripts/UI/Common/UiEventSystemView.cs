using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AsteroidGame.Scripts.UI.Common
{
    [DisallowMultipleComponent]
    public sealed class UiEventSystemView : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private BaseInputModule[] _inputModules;

        private void Awake()
        {
            ValidateRequiredReferences();
            Hide();
        }

        private void OnValidate()
        {
            if (_eventSystem == null)
                _eventSystem = GetComponent<EventSystem>();

            if (_inputModules == null || _inputModules.Length == 0)
                _inputModules = GetComponents<BaseInputModule>();

            if (_eventSystem == null)
                Debug.LogError($"{nameof(UiEventSystemView)} on {name} requires EventSystem.", this);

            if (_inputModules == null || _inputModules.Length == 0)
                Debug.LogError($"{nameof(UiEventSystemView)} on {name} requires input modules.", this);
        }

        public void Show()
        {
            ValidateRequiredReferences();

            _eventSystem.enabled = true;

            for (int i = 0; i < _inputModules.Length; i++)
                _inputModules[i].enabled = true;
        }

        public void Hide()
        {
            ValidateRequiredReferences();

            _eventSystem.enabled = false;

            for (int i = 0; i < _inputModules.Length; i++)
                _inputModules[i].enabled = false;
        }

        private void ValidateRequiredReferences()
        {
            if (_eventSystem == null)
                throw new InvalidOperationException($"{nameof(UiEventSystemView)} requires EventSystem.");

            if (_inputModules == null || _inputModules.Length == 0)
                throw new InvalidOperationException($"{nameof(UiEventSystemView)} requires input modules.");

            for (int i = 0; i < _inputModules.Length; i++)
            {
                if (_inputModules[i] == null)
                {
                    throw new InvalidOperationException($"{nameof(UiEventSystemView)}" +
                                                        $" has missing input module reference.");
                }
            }
        }
    }
}