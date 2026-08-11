using System;
using AsteroidGame.Scripts.Gameplay.Bullets.Contracts;

namespace AsteroidGame.Scripts.Presentation.Bullets
{
    public sealed class BulletViewFactoryAdapter : IBulletViewFactory
    {
        private readonly BulletViewPrefabFactory _prefabFactory;

        public BulletViewFactoryAdapter(BulletViewPrefabFactory prefabFactory) => _prefabFactory = prefabFactory;

        public IBulletView Create()
        {
            BulletView bulletView = _prefabFactory.Create();

            if (bulletView == null)
                throw new InvalidOperationException("BulletViewPrefabFactory returned null BulletView.");

            return bulletView;
        }
    }
}