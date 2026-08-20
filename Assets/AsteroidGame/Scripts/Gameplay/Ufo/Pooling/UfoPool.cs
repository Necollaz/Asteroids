using System;
using Zenject;
using System.Collections.Generic;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Settings;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Factories;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Pooling
{
    public sealed class UfoPool : IInitializable
    {
        private readonly EnemySpawnSettings _settings;
        private readonly UfoInstanceFactory _instanceFactory;
        private readonly CollisionBodyRegistry _collisionRegistry;
        private readonly Queue<UfoInstance> _availableUfo = new();
        private readonly List<UfoInstance> _activeUfos = new();

        private int _createdCount;

        public UfoPool(
            EnemySpawnSettings settings,
            UfoInstanceFactory instanceFactory,
            CollisionBodyRegistry collisionRegistry)
        {
            _settings = settings;
            _instanceFactory = instanceFactory;
            _collisionRegistry = collisionRegistry;
        }

        public IReadOnlyList<UfoInstance> ActiveUfos => _activeUfos;

        void IInitializable.Initialize()
        {
            for (int i = 0; i < _settings.UfoPoolSize; i++)
                _availableUfo.Enqueue(CreateUfo());
        }

        public bool TrySpawn(Vector2D position, Velocity velocity, float rotationDegrees)
        {
            if (_activeUfos.Count >= _settings.MaxActiveUfo)
                return false;

            if (!TryGet(out UfoInstance ufo))
                return false;

            ufo.Activate(position, velocity, rotationDegrees);

            return true;
        }

        public bool TryGetByCollisionBody(CollisionBody body, out UfoInstance ufo)
        {
            ufo = null;
            
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            for (int i = _activeUfos.Count - 1; i >= 0; i--)
            {
                UfoInstance current = _activeUfos[i];
                
                if (!ReferenceEquals(current.CollisionBody, body))
                    continue;
                
                ufo = current;
                
                return true;
            }
            
            return false;
        }

        public bool ReleaseByCollisionBody(CollisionBody body, out UfoInstance ufo)
        {
            ufo = null;

            if (body == null)
                throw new ArgumentNullException(nameof(body));

            for (int i = _activeUfos.Count - 1; i >= 0; i--)
            {
                UfoInstance current = _activeUfos[i];

                if (!ReferenceEquals(current.CollisionBody, body))
                    continue;

                ufo = current;
                Release(current);

                return true;
            }

            return false;
        }

        private UfoInstance CreateUfo()
        {
            UfoInstance ufo = _instanceFactory.Create();

            ufo.Deactivate();
            _collisionRegistry.Register(ufo.CollisionBody);
            _createdCount++;

            return ufo;
        }
        
        private bool TryGet(out UfoInstance ufo)
        {
            ufo = null;

            if (_availableUfo.Count == 0 && _createdCount >= _settings.UfoPoolSize)
                return false;

            ufo = _availableUfo.Count > 0 ? _availableUfo.Dequeue() : CreateUfo();
            _activeUfos.Add(ufo);

            return true;
        }
        
        private void Release(UfoInstance ufo)
        {
            if (ufo == null)
                throw new ArgumentNullException(nameof(ufo));

            if (!_activeUfos.Remove(ufo))
                throw new InvalidOperationException("UFO is already released or does not belong to active pool.");

            ufo.Deactivate();
            _availableUfo.Enqueue(ufo);
        }
    }
}