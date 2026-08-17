using System;
using UnityEngine;
using AsteroidGame.Scripts.Domain.Bullets.Contracts;
using AsteroidGame.Scripts.Domain.Player.Contracts;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Sections
{
    [Serializable]
    public sealed class WeaponSettingsConfigSection : IBulletSettingsData, IPlayerLaserSettingsData
    {
        [Header("Bullets")]
        [SerializeField] private int _bulletPoolSize = 24;
        [SerializeField] private float _bulletSpeed = 24f;
        [SerializeField] private float _bulletLifetimeSeconds = 1.2f;
        [SerializeField] private float _bulletRadius = 0.2f;
        [SerializeField] private float _bulletShotsPerSecond = 5f;
        [SerializeField] private float _bulletSpawnOffset = 0.7f;
        [SerializeField] private float _bulletVisibilityMargin = 0.25f;

        [Header("Laser")]
        [SerializeField] private int _maxLaserCharges = 3;
        [SerializeField] private int _initialLaserCharges = 3;
        [SerializeField] private float _laserRechargeSeconds = 4f;
        [SerializeField] private float _laserVisibleSeconds = 1f;
        [SerializeField] private float _laserLength = 2.5f;
        [SerializeField] private float _laserHitHalfWidth = 0.15f;
        [SerializeField] private float _laserVisualWidth = 0.25f;

        public int PoolSize => _bulletPoolSize;
        public int PlayerMaxLaserCharges => _maxLaserCharges;
        public int PlayerInitialLaserCharges => _initialLaserCharges;
        public float BulletSpeed => _bulletSpeed;
        public float BulletLifetimeSeconds => _bulletLifetimeSeconds;
        public float BulletRadius => _bulletRadius;
        public float BulletShotsPerSecond => _bulletShotsPerSecond;
        public float BulletSpawnOffset => _bulletSpawnOffset;
        public float BulletVisibilityMargin => _bulletVisibilityMargin;
        public float PlayerLaserRechargeSeconds => _laserRechargeSeconds;
        public float PlayerLaserVisibleSeconds => _laserVisibleSeconds;
        public float PlayerLaserLength => _laserLength;
        public float PlayerLaserHitHalfWidth => _laserHitHalfWidth;
        public float PlayerLaserVisualWidth => _laserVisualWidth;
    }
}