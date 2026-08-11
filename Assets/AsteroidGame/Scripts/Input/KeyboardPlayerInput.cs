using System;
using UnityEngine;
using Zenject;
using AsteroidGame.Scripts.Domain.Player.Models;

namespace AsteroidGame.Scripts.Input
{
    public sealed class KeyboardPlayerInput : MonoBehaviour, IPlayerInput
    {
        private IKeyboardInputSettingsData _settingsData;

        [Inject] private void Construct(IKeyboardInputSettingsData settingsData) => 
            _settingsData = settingsData ?? throw new ArgumentNullException(nameof(settingsData));

        public PlayerInputState GetState()
        {
            float turnDirection = 0f;

            if (UnityEngine.Input.GetKey(_settingsData.TurnLeftKey) ||
                UnityEngine.Input.GetKey(_settingsData.AlternativeTurnLeftKey))
            {
                turnDirection = 1f;
            }

            if (UnityEngine.Input.GetKey(_settingsData.TurnRightKey) ||
                UnityEngine.Input.GetKey(_settingsData.AlternativeTurnRightKey))
            {
                turnDirection = -1f;
            }

            bool isThrustPressed =
                UnityEngine.Input.GetKey(_settingsData.ThrustKey) ||
                UnityEngine.Input.GetKey(_settingsData.AlternativeThrustKey);

            bool isBulletFirePressed =
                UnityEngine.Input.GetKey(_settingsData.FireBulletKey) ||
                UnityEngine.Input.GetKey(_settingsData.AlternativeFireBulletKey);

            return new PlayerInputState(turnDirection, isThrustPressed, isBulletFirePressed);
        }
    }
}