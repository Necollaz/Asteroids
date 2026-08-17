using System;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Sections;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json
{
    public sealed class GameplayJsonSettings
    {
        public GameplayJsonSettings(
            JsonPlayerSettingsSection player,
            JsonWeaponSettingsSection weapons,
            JsonEnemySettingsSection enemies,
            JsonWorldSettingsSection world)
        {
            Player = player ?? throw new ArgumentNullException(nameof(player));
            Weapons = weapons ?? throw new ArgumentNullException(nameof(weapons));
            Enemies = enemies ?? throw new ArgumentNullException(nameof(enemies));
            World = world ?? throw new ArgumentNullException(nameof(world));
        }

        public JsonPlayerSettingsSection Player { get; }
        public JsonWeaponSettingsSection Weapons { get; }
        public JsonEnemySettingsSection Enemies { get; }
        public JsonWorldSettingsSection World { get; }
    }
}