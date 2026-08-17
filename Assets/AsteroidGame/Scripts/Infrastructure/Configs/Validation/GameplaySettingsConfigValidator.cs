using Zenject;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Validation
{
    public sealed class GameplaySettingsConfigValidator : IInitializable
    {
        private readonly GameplaySettingsConfig _config;
        private readonly PlayerSettingsValidator _playerValidator;
        private readonly WeaponSettingsValidator _weaponValidator;
        private readonly EnemySettingsValidator _enemyValidator;
        private readonly WorldSettingsValidator _worldValidator;

        public GameplaySettingsConfigValidator(
            GameplaySettingsConfig config,
            PlayerSettingsValidator playerValidator,
            WeaponSettingsValidator weaponValidator,
            EnemySettingsValidator enemyValidator,
            WorldSettingsValidator worldValidator)
        {
            _config = config;
            _playerValidator = playerValidator;
            _weaponValidator = weaponValidator;
            _enemyValidator = enemyValidator;
            _worldValidator = worldValidator;
        }

        void IInitializable.Initialize()
        {
            if (_config.SettingsSource != GameplaySettingsSource.ScriptableObject)
                return;

            _playerValidator.Validate(_config.Player, _config.Player, _config.Player, _config.Player);
            _weaponValidator.Validate(_config.Weapons, _config.Weapons);
            _enemyValidator.Validate(_config.Enemies, _config.Enemies, _config.Enemies);
            _worldValidator.Validate(_config.World);
        }
    }
}