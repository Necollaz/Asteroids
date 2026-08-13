using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Presentation.Ufo.Effects
{
    public sealed class UfoExplosionView : MonoBehaviour
    {
        private const string DefaultExplosionStateName = "Explosion_01";
        
        [SerializeField] private Animator _animator;
        [SerializeField] private string _explosionStateName = DefaultExplosionStateName;
        [SerializeField] private float _durationSeconds = 0.6f;
        
        public float DurationSeconds => _durationSeconds;

        private void Awake()
        {
            Validate();
            Hide();
        }

        public void Play(Vector2D position)
        {
            transform.position = new Vector3(position.X, position.Y, transform.position.z);
            gameObject.SetActive(true);
            
            _animator.Rebind();
            _animator.Update(0f);
            _animator.Play(_explosionStateName, 0, 0f);
        }
        
        public void Hide() => gameObject.SetActive(false);

        private void OnValidate()
        {
            if (_animator == null)
                TryGetComponent(out _animator);

            if (string.IsNullOrWhiteSpace(_explosionStateName))
                _explosionStateName = DefaultExplosionStateName;
        }

        private void Validate()
        {
            if (_animator == null)
                throw new InvalidOperationException($"{nameof(UfoExplosionView)} requires Animator.");

            if (string.IsNullOrWhiteSpace(_explosionStateName))
                throw new InvalidOperationException($"{nameof(UfoExplosionView)} requires animation state name.");

            if (_durationSeconds <= 0f)
                throw new InvalidOperationException($"{nameof(UfoExplosionView)} requires positive duration.");
        }
    }
}