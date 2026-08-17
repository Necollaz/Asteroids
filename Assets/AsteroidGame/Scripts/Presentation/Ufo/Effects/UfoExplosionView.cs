using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Physics.Models;

namespace AsteroidGame.Scripts.Presentation.Ufo.Effects
{
    public sealed class UfoExplosionView : MonoBehaviour
    {
        private const float DefaultDurationSeconds = 0.6f;
        private const float StartAnimationTime = 0f;
        
        [SerializeField] private Animator _animator;
        [SerializeField] private float _durationSeconds = DefaultDurationSeconds;
        
        public float DurationSeconds => _durationSeconds;

        private void Awake()
        {
            Validate();
            
            Hide();
        }

        private void OnValidate()
        {
            if (_animator == null)
                TryGetComponent(out _animator);

            if (_durationSeconds <= 0f)
                _durationSeconds = DefaultDurationSeconds;
        }

        public void Play(Vector2D position)
        {
            Validate();

            transform.position = new Vector3(position.X, position.Y, transform.position.z);
            
            gameObject.SetActive(true);
            _animator.Rebind();
            _animator.Update(StartAnimationTime);
        }

        public void Hide() => gameObject.SetActive(false);

        private void Validate()
        {
            if (_animator == null)
                throw new InvalidOperationException($"{nameof(UfoExplosionView)} requires Animator.");

            if (_animator.runtimeAnimatorController == null)
                throw new InvalidOperationException($"{nameof(UfoExplosionView)} requires Animator Controller.");

            if (_durationSeconds <= 0f)
                throw new InvalidOperationException($"{nameof(UfoExplosionView)} requires positive duration.");
        }
    }
}