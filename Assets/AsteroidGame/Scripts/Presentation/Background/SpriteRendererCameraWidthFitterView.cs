using System;
using UnityEngine;

namespace AsteroidGame.Scripts.Presentation.Background
{
    [DisallowMultipleComponent]
    public sealed class SpriteRendererCameraWidthFitterView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private float _widthMultiplier = 1f;

        private void Awake() => FitToCameraWidth();

        private void OnValidate()
        {
            if (_spriteRenderer == null)
                TryGetComponent(out _spriteRenderer);

            if (_widthMultiplier <= 0f)
                _widthMultiplier = 1f;
        }

        public void FitToCameraWidth()
        {
            ValidateRequiredReferences();

            float cameraWidth = _camera.orthographicSize * 2f * _camera.aspect;
            float spriteWidth = _spriteRenderer.sprite.bounds.size.x;

            if (spriteWidth <= 0f)
            {
                throw new InvalidOperationException(
                    $"{nameof(SpriteRendererCameraWidthFitterView)} cannot calculate sprite width.");
            }

            float uniformScale = cameraWidth / spriteWidth * _widthMultiplier;
            Vector3 currentScale = transform.localScale;

            transform.localScale = new Vector3(uniformScale, uniformScale, currentScale.z);
        }

        private void ValidateRequiredReferences()
        {
            if (_camera == null)
                throw new InvalidOperationException($"{nameof(SpriteRendererCameraWidthFitterView)} requires Camera.");

            if (!_camera.orthographic)
            {
                throw new InvalidOperationException(
                    $"{nameof(SpriteRendererCameraWidthFitterView)} supports only orthographic Camera.");
            }

            if (_spriteRenderer == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(SpriteRendererCameraWidthFitterView)} requires SpriteRenderer.");
            }

            if (_spriteRenderer.sprite == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(SpriteRendererCameraWidthFitterView)} requires SpriteRenderer sprite.");
            }
        }
    }
}