using System;
using UnityEngine;
using AsteroidGame.Scripts.Infrastructure.Configs.Sections;
using AsteroidGame.Scripts.Infrastructure.Core;

namespace AsteroidGame.Scripts.Infrastructure.Configs
{
    [CreateAssetMenu(
        fileName = nameof(GameplaySettingsConfig),
        menuName = CoreConstants.EditorConfigsPath + nameof(GameplaySettingsConfig))]
    public sealed class GameplaySettingsConfig : ScriptableObject
    {
        [Header("Settings Source")]
        [SerializeField] private GameplaySettingsSource _settingsSource = GameplaySettingsSource.ScriptableObject;
        
        [Header("JSON")]
        [SerializeField] private TextAsset _playerSettingsJson;
        [SerializeField] private TextAsset _enemiesSettingsJson;
        [SerializeField] private TextAsset _worldSettingsJson;

        [Header("ScriptableObject Sections")]
        [SerializeField] private PlayerSettingsConfigSection _player = new PlayerSettingsConfigSection();
        [SerializeField] private WeaponSettingsConfigSection _weapons = new WeaponSettingsConfigSection();
        [SerializeField] private EnemySettingsConfigSection _enemies = new EnemySettingsConfigSection();
        [SerializeField] private WorldSettingsConfigSection _world = new WorldSettingsConfigSection();

        public GameplaySettingsSource SettingsSource => _settingsSource;

        public TextAsset PlayerSettingsJson => RequireTextAsset(_playerSettingsJson, nameof(_playerSettingsJson));
        public TextAsset EnemiesSettingsJson => RequireTextAsset(_enemiesSettingsJson, nameof(_enemiesSettingsJson));
        public TextAsset WorldSettingsJson => RequireTextAsset(_worldSettingsJson, nameof(_worldSettingsJson));

        public PlayerSettingsConfigSection Player => RequireSection(_player, nameof(_player));
        public WeaponSettingsConfigSection Weapons => RequireSection(_weapons, nameof(_weapons));
        public EnemySettingsConfigSection Enemies => RequireSection(_enemies, nameof(_enemies));
        public WorldSettingsConfigSection World => RequireSection(_world, nameof(_world));

        private void OnValidate()
        {
            EnsureSectionsCreated();

            if (_settingsSource == GameplaySettingsSource.Json)
            {
                ValidateJsonReferences();

                return;
            }

            ValidateSections();
        }

        private void EnsureSectionsCreated()
        {
            _player ??= new PlayerSettingsConfigSection();
            _weapons ??= new WeaponSettingsConfigSection();
            _enemies ??= new EnemySettingsConfigSection();
            _world ??= new WorldSettingsConfigSection();
        }

        private void ValidateSections()
        {
            if (_player == null)
                Debug.LogError($"{nameof(GameplaySettingsConfig)} requires player settings section.", this);

            if (_weapons == null)
                Debug.LogError($"{nameof(GameplaySettingsConfig)} requires weapon settings section.", this);

            if (_enemies == null)
                Debug.LogError($"{nameof(GameplaySettingsConfig)} requires enemy settings section.", this);

            if (_world == null)
                Debug.LogError($"{nameof(GameplaySettingsConfig)} requires world settings section.", this);
        }

        private void ValidateJsonReferences()
        {
            if (_playerSettingsJson == null)
                Debug.LogError($"{nameof(GameplaySettingsConfig)} requires player settings json.", this);

            if (_enemiesSettingsJson == null)
                Debug.LogError($"{nameof(GameplaySettingsConfig)} requires enemies settings json.", this);

            if (_worldSettingsJson == null)
                Debug.LogError($"{nameof(GameplaySettingsConfig)} requires world settings json.", this);
        }

        private TSection RequireSection<TSection>(TSection section, string fieldName)
            where TSection : class
        {
            if (section == null)
                throw new InvalidOperationException($"{nameof(GameplaySettingsConfig)} requires {fieldName}.");

            return section;
        }

        private TextAsset RequireTextAsset(TextAsset textAsset, string fieldName)
        {
            if (textAsset == null)
                throw new InvalidOperationException($"{nameof(GameplaySettingsConfig)} requires {fieldName}.");

            return textAsset;
        }
    }
}