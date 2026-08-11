using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Player.Models;

namespace AsteroidGame.Scripts.Presentation.Player.Views
{
    [DisallowMultipleComponent]
    public sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _rotatingRoot;

        private void Awake() => ValidateRequiredReferences();

        private void OnValidate()
        {
            if (_rotatingRoot == null)
                Debug.LogError($"{nameof(PlayerView)} on {name} requires rotating root.", this);
        }

        public void ApplySnapshot(PlayerSnapshot snapshot)
        {
            ValidateRequiredReferences();

            transform.position = new Vector3(snapshot.Position.X, snapshot.Position.Y, 0f);
            _rotatingRoot.rotation = Quaternion.Euler(0f, 0f, snapshot.RotationDegrees);
        }

        private void ValidateRequiredReferences()
        {
            if (_rotatingRoot == null)
                throw new InvalidOperationException($"{nameof(PlayerView)} requires rotating root.");
        }
    }
}