using System;
using UnityEngine;
using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Factories;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Laser.Contracts;

namespace AsteroidGame.Scripts.Presentation.Laser
{
    [DisallowMultipleComponent]
    public sealed class PlayerLaserSpawnPointView : MonoBehaviour, ILaserSpawnPointProvider
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _directionRoot;

        private PhysicsValueFactory _physicsValueFactory;

        public Vector2D Position
        {
            get
            {
                ValidateRequiredReferences();

                Vector3 position = _spawnPoint.position;

                return _physicsValueFactory.CreateVector(position.x, position.y);
            }
        }
        public Vector2D Direction
        {
            get
            {
                ValidateRequiredReferences();

                Vector3 direction = _directionRoot.up;

                return _physicsValueFactory.CreateVector(direction.x, direction.y).Normalized;
            }
        }

        [Inject] private void Construct(PhysicsValueFactory physicsValueFactory) =>
            _physicsValueFactory = physicsValueFactory ?? throw new ArgumentNullException(nameof(physicsValueFactory));

        private void Awake() => ValidateRequiredReferences();

        private void OnValidate()
        {
            if (_spawnPoint == null)
                _spawnPoint = transform;
            
            if (_directionRoot == null)
                _directionRoot = _spawnPoint;
        }

        private void ValidateRequiredReferences()
        {
            if (_spawnPoint == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserSpawnPointView)} requires spawn point.");

            if (_directionRoot == null)
                throw new InvalidOperationException($"{nameof(PlayerLaserSpawnPointView)} requires direction root.");

            if (_physicsValueFactory == null && Application.isPlaying)
            {
                throw new InvalidOperationException(
                    $"{nameof(PlayerLaserSpawnPointView)} requires PhysicsValueFactory.");
            }
        }
    }
}