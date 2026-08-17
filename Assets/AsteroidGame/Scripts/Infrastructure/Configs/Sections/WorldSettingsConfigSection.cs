using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.World;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Sections
{
    [Serializable]
    public sealed class WorldSettingsConfigSection : IWorldSettingsData
    {
        [SerializeField] private float _worldWidth = 18f;
        [SerializeField] private float _worldHeight = 10f;

        public float WorldWidth => _worldWidth;
        public float WorldHeight => _worldHeight;
    }
}