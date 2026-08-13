using System;
using UnityEngine;
using Zenject;
using AsteroidGame.Scripts.Gameplay.Ufo.Contracts;

namespace AsteroidGame.Scripts.Presentation.Ufo
{
    public sealed class UfoViewFactoryAdapter : IUfoViewFactory
    {
        private readonly DiContainer _container;
        private readonly UfoView _prefab;
        private readonly Transform _root;

        public UfoViewFactoryAdapter(DiContainer container, UfoView prefab, Transform root)
        {
            _container = container;
            _prefab = prefab;
            _root = root;
        }

        public IUfoView Create()
        {
            UfoView view = _container.InstantiatePrefabForComponent<UfoView>(_prefab, _root);

            if (view == null)
                throw new InvalidOperationException("Failed to create UFO view.");

            return view;
        }
    }
}