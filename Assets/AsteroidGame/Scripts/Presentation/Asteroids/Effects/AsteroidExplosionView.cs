using System;
using AsteroidGame.Scripts.Domain.Physics.Models;
using UnityEngine;

namespace AsteroidGame.Scripts.Presentation.Asteroids.Effects
{
    [DisallowMultipleComponent]
    public class AsteroidExplosionView : MonoBehaviour
    {
        private const float DefaultDurationSeconds = 1.5f;
        
        [SerializeField] private ParticleSystem[] _particles;
        [SerializeField] private float _durationSeconds = DefaultDurationSeconds;
        
        public float DurationSeconds => _durationSeconds;

        private void Awake()
        {
            ValidateRequiredReferences();
            Hide();
        }

        private void OnValidate()
        {
            if (_particles == null || _particles.Length == 0)
                _particles = GetComponentsInChildren<ParticleSystem>(true);

            if (_durationSeconds <= 0f)
                _durationSeconds = DefaultDurationSeconds;
        }

        public void Play(Vector2D position)
        {
            ValidateRequiredReferences();

            transform.position = new Vector3(position.X, position.Y, transform.position.z);
            gameObject.SetActive(true);

            for (int i = 0; i < _particles.Length; i++)
            {
                _particles[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                _particles[i].Play(true);
            }
        }

        public void Hide()
        {
            ValidateRequiredReferences();

            for (int i = 0; i < _particles.Length; i++)
                _particles[i].Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            
            gameObject.SetActive(false);
        }

        private void ValidateRequiredReferences()
        {
            if (_particles == null || _particles.Length == 0)
                throw new InvalidOperationException($"{nameof(AsteroidExplosionView)} requires particle systems.");

            for (int i = 0; i < _particles.Length; i++)
            {
                if (_particles[i] == null)
                    throw new InvalidOperationException($"{nameof(AsteroidExplosionView)} has null particle system.");
            }

            if (_durationSeconds <= 0f)
                throw new InvalidOperationException($"{nameof(AsteroidExplosionView)} requires positive duration.");
        }
    }
}