using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.World;

namespace AsteroidGame.Scripts.Presentation.Camera
{
    [DisallowMultipleComponent]
    public sealed class CameraBoundsView : MonoBehaviour, IWorldSettingsData
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public float WorldWidth => GetWorldWidth();
        public float WorldHeight => GetWorldHeight();

        private void OnValidate() => _camera ??= GetComponent<UnityEngine.Camera>();

        private float GetWorldWidth()
        {
            ValidateCamera();

            return _camera.orthographicSize * 2f * _camera.aspect;
        }

        private float GetWorldHeight()
        {
            ValidateCamera();

            return _camera.orthographicSize * 2f;
        }

        private void ValidateCamera()
        {
            if (_camera == null)
                throw new InvalidOperationException("CameraBoundsView requires assigned Camera.");

            if (!_camera.orthographic)
                throw new InvalidOperationException("CameraBoundsView supports only orthographic camera.");
        }
    }
}