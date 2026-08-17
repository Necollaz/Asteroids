using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Presentation.Laser
{
    [DisallowMultipleComponent]
    public sealed class PlayerLaserView : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _beamRoot;
        [SerializeField] private SpriteRenderer _beamRenderer;

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

            Vector2 spriteSize = GetBeamSpriteSize();
            Vector3 normalizedDirection = direction / length;

            _root.gameObject.SetActive(true);
            _root.position = start;
            _root.rotation = Quaternion.FromToRotation(Vector3.right, normalizedDirection);

            _beamRenderer.drawMode = SpriteDrawMode.Simple;
            _beamRoot.localPosition = new Vector3(0f, 0f, _beamRoot.localPosition.z);
            _beamRoot.localRotation = Quaternion.identity;
            _beamRoot.localScale = new Vector3(
                length / spriteSize.x,
                visualWidth / spriteSize.y, 
                _beamRoot.localScale.z);
        }

        public void Hide()
        {
            ValidateRequiredReferences();
            _root.gameObject.SetActive(false);
        }
        
        private Vector2 GetBeamSpriteSize()
        {
            if (_beamRenderer.sprite == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserView)} requires beam sprite.");

            Vector2 size = _beamRenderer.sprite.bounds.size;

            if (size.x <= 0f || size.y <= 0f)
                throw new InvalidOperationException($"{nameof(PlayerLaserView)} cannot calculate beam sprite size.");

            return size;
        }

        private void ValidateRequiredReferences()
        {
            if (_root == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserView)} requires root.");

            if (_beamRoot == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserView)} requires beam root.");

            if (_beamRenderer == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserView)} requires beam renderer.");
        }
    }
}