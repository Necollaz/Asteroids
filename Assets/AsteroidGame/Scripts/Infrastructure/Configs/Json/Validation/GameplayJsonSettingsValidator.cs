using System;
using AsteroidGame.Scripts.Infrastructure.Configs.Validation;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Validation
{
    public sealed class GameplayJsonSettingsValidator
    {
        private readonly PlayerSettingsValidator _playerValidator;
        private readonly WeaponSettingsValidator _weaponValidator;
        private readonly EnemySettingsValidator _enemyValidator;
        private readonly SpawnSettingsValidator _spawnValidator;
        private readonly WorldSettingsValidator _worldValidator;

        public GameplayJsonSettingsValidator(
            PlayerSettingsValidator playerValidator,
            WeaponSettingsValidator weaponValidator,
            EnemySettingsValidator enemyValidator,
            SpawnSettingsValidator spawnValidator,
            WorldSettingsValidator worldValidator)
        {
            _playerValidator = playerValidator ?? throw new ArgumentNullException(nameof(playerValidator));
            _weaponValidator = weaponValidator ?? throw new ArgumentNullException(nameof(weaponValidator));
            _enemyValidator = enemyValidator ?? throw new ArgumentNullException(nameof(enemyValidator));
            _spawnValidator = spawnValidator ?? throw new ArgumentNullException(nameof(spawnValidator));
            _worldValidator = worldValidator ?? throw new ArgumentNullException(nameof(worldValidator));
        }

        public void Validate(GameplayJsonSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException(nameof(settings));

            _playerValidator.Validate(settings.Player, settings.Player, settings.Player, settings.Player);
            _weaponValidator.Validate(settings.Weapons, settings.Weapons);
            _enemyValidator.Validate(settings.Enemies, settings.Enemies, settings.Enemies);
            _spawnValidator.Validate(settings.Spawn);
            _worldValidator.Validate(settings.World);
        }
    }
}