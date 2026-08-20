using AsteroidGame.Scripts.Domain.Collision.Types;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Ufo.Models;
using AsteroidGame.Scripts.Domain.Ufo.Settings;
using AsteroidGame.Scripts.Gameplay.Enemies.Factories;
using AsteroidGame.Scripts.Gameplay.Ufo.Contracts;
using AsteroidGame.Scripts.Gameplay.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.States;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Factories
{
    public sealed class UfoInstanceFactory
    {
        private readonly EnemyInstanceContextFactory _enemyContextFactory;
        private readonly UfoModelFactory _ufoModelFactory;
        private readonly UfoInstanceZenjectFactory _ufoInstanceFactory;
        private readonly UfoKnockbackStateFactory _knockbackStateFactory;
        private readonly IUfoViewFactory _viewFactory;
        private readonly UfoSettings _settings;

        public UfoInstanceFactory(
            EnemyInstanceContextFactory enemyContextFactory,
            UfoModelFactory ufoModelFactory,
            UfoInstanceZenjectFactory ufoInstanceFactory,
            UfoKnockbackStateFactory knockbackStateFactory,
            IUfoViewFactory viewFactory,
            UfoSettings settings)
        {
            _enemyContextFactory = enemyContextFactory;
            _ufoModelFactory = ufoModelFactory;
            _ufoInstanceFactory = ufoInstanceFactory;
            _knockbackStateFactory = knockbackStateFactory;
            _viewFactory = viewFactory;
            _settings = settings;
        }

        public UfoInstance Create()
        {
            EnemyInstanceContext context = _enemyContextFactory.Create(
                EnemyType.Ufo,
                CollisionCategory.Ufo,
                _settings.CollisionRadius);
            UfoModel ufo = _ufoModelFactory.Create(context.Enemy);
            IUfoView view = _viewFactory.Create();
            UfoKnockbackState knockbackState = _knockbackStateFactory.Create();

            return _ufoInstanceFactory.Create(ufo, context.CollisionBody, view, knockbackState);
        }
    }
}