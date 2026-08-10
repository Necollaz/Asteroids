using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Collision;
using AsteroidGame.Scripts.Domain.Player;
using AsteroidGame.Scripts.Gameplay.Factories;

namespace AsteroidGame.Scripts.Gameplay.Player
{
    public sealed class PlayerCollisionBodyService : IInitializable, IDisposable
    {
        private readonly CollisionBodyRegistry _registry;
        private readonly CollisionBodyFactory _collisionBodyFactory;
        private readonly PlayerCollisionSettings _settings;
        private readonly IPlayerBodyProvider _playerBodyProvider;
        
        private CollisionBody _collisionBody;

        public PlayerCollisionBodyService(
            CollisionBodyRegistry registry,
            CollisionBodyFactory collisionBodyFactory,
            PlayerCollisionSettings settings,
            IPlayerBodyProvider playerBodyProvider)
        {
            _registry = registry;
            _collisionBodyFactory = collisionBodyFactory;
            _settings = settings;
            _playerBodyProvider = playerBodyProvider;
        }

        void IInitializable.Initialize()
        {
            _collisionBody = _collisionBodyFactory.Create(
                CollisionCategory.Player,
                _playerBodyProvider.Body,
                _settings.CollisionRadius);
            _registry.Register(_collisionBody);
        }

        void IDisposable.Dispose()
        {
            if (_collisionBody != null)
                _registry.Unregister(_collisionBody);
        }
    }
}