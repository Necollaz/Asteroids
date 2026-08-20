using System;
using UnityEngine;
using Zenject;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Gameplay.Asteroids.Contracts;

namespace AsteroidGame.Scripts.Presentation.Asteroids
{
    public sealed class AsteroidViewFactoryAdapter : IAsteroidViewFactory
    {
        private readonly DiContainer _container;
        private readonly AsteroidView _largePrefab;
        private readonly AsteroidView _mediumPrefab;
        private readonly AsteroidView _smallPrefab;
        private readonly Transform _root;

        public AsteroidViewFactoryAdapter(
            DiContainer container,
            AsteroidView largePrefab,
            AsteroidView mediumPrefab,
            AsteroidView smallPrefab,
            Transform root)
        {
            _container = container;
            _largePrefab = largePrefab;
            _mediumPrefab = mediumPrefab;
            _smallPrefab = smallPrefab;
            _root = root;
        }

        public IAsteroidView Create(EnemyType type)
        {
            AsteroidView prefab = GetPrefab(type);
            AsteroidView view = _container.InstantiatePrefabForComponent<AsteroidView>(prefab, _root);
            
            if (view == null)
                throw new InvalidOperationException($"Failed to create asteroid view for type {type}.");

            return view;
        }

        private AsteroidView GetPrefab(EnemyType type)
        {
            return type switch
            {
                EnemyType.LargeAsteroid => _largePrefab,
                EnemyType.MediumAsteroid => _mediumPrefab,
                EnemyType.SmallAsteroid => _smallPrefab,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
        }
    }
}