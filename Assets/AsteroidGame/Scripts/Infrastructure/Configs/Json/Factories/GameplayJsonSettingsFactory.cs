using System;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.World;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Loading;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Factories
{
    public sealed class GameplayJsonSettingsFactory
    {
        private readonly KeyCodeParser _keyCodeParser;

        public GameplayJsonSettingsFactory(KeyCodeParser keyCodeParser) =>
            _keyCodeParser = keyCodeParser ?? throw new ArgumentNullException(nameof(keyCodeParser));

        public GameplayJsonSettings Create(
            PlayerSettingsJson player,
            EnemiesSettingsJson enemies,
            WorldSettingsJson world)
        {
            JsonPlayerSettingsSection playerSection = new (player, _keyCodeParser);
            JsonWeaponSettingsSection weaponSection = new (player);
            JsonEnemySettingsSection enemySection = new (enemies);
            JsonWorldSettingsSection worldSection = new (world);
            JsonEnemySpawnSettingsSection spawnSection = new (world);

            return new GameplayJsonSettings(
                playerSection,
                weaponSection,
                enemySection,
                worldSection,
                spawnSection);
        }
    }
}