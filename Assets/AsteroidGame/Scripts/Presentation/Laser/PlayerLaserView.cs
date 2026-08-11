using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Presentation.Laser
{
    [DisallowMultipleComponent]
    public sealed class PlayerLaserView : MonoBehaviour
    {
        private const float DefaultVisualStartOffset = 0.35f;

        [SerializeField] private Transform _root;
        [SerializeField] private Transform _beamRoot;
        [SerializeField] private SpriteRenderer _beamRenderer;
        [SerializeField] private float _visualStartOffset = DefaultVisualStartOffset;

        private void Awake()
        {
            ValidateRequiredReferences();
            Hide();
        }

        private void OnValidate()
        {
            if (_root == null)
                _root = transform;

            if (_beamRoot == null)
                _beamRoot = transform;

            if (_beamRenderer == null && _beamRoot != null)
                _beamRoot.TryGetComponent(out _beamRenderer);

            if (_visualStartOffset < 0f)
                _visualStartOffset = 0f;
        }

        public void Show(Vector2D startPosition, Vector2D endPosition, float visualWidth)
        {
            ValidateRequiredReferences();

            Vector3 start = new Vector3(startPosition.X, startPosition.Y, _root.position.z);
            Vector3 end = new Vector3(endPosition.X, endPosition.Y, _root.position.z);
            Vector3 direction = end - start;
            float length = direction.magnitude;

            if (length <= float.Epsilon)
                throw new InvalidOperationException("Laser visual length is too small.");

            float beamSpriteLength = GetBeamSpriteLength();
            Vector3 normalizedDirection = direction / length;
            Vector3 visualStart = start + normalizedDirection * _visualStartOffset;
            
            _root.gameObject.SetActive(true);
            
            _root.position = visualStart;
            _root.rotation = Quaternion.FromToRotation(Vector3.right, normalizedDirection);
            _beamRoot.localPosition = new Vector3(length * 0.5f, 0f, _beamRoot.localPosition.z);
            _beamRoot.localRotation = Quaternion.identity;
            _beamRoot.localScale = new Vector3(length / beamSpriteLength, visualWidth, _beamRoot.localScale.z);
        }

        public void Hide()
        {
            ValidateRequiredReferences();
            _root.gameObject.SetActive(false);
        }

        private float GetBeamSpriteLength()
        {
            if (_beamRenderer.size.x > 0f)
                return _beamRenderer.size.x;

            if (_beamRenderer.sprite != null && _beamRenderer.sprite.bounds.size.x > 0f)
                return _beamRenderer.sprite.bounds.size.x;

            throw new InvalidOperationException($"{nameof(PlayerLaserView)} cannot calculate beam sprite length.");
        }

        private void ValidateRequiredReferences()
        {
            if (_root == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserView)} requires root.");

            if (_beamRoot == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserView)} requires beam root.");

            if (_beamRenderer == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserView)} requires beam renderer.");

            if (_visualStartOffset < 0f)
            {
                throw new InvalidOperationException(
                    $"{nameof(PlayerLaserView)} requires non-negative visual start offset.");
            }
        }
    }
}