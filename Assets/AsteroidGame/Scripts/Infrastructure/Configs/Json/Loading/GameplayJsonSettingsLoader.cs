using System;
using UnityEngine;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Enemy;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.Player;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Data.World;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Factories;
using AsteroidGame.Scripts.Infrastructure.Configs.Json.Validation;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Loading
{
    public sealed class GameplayJsonSettingsLoader
    {
        private readonly GameplayJsonSettingsFactory _settingsFactory;
        private readonly GameplayJsonSettingsValidator _validator;

        public GameplayJsonSettingsLoader(
            GameplayJsonSettingsFactory settingsFactory, 
            GameplayJsonSettingsValidator validator)
        {
            _settingsFactory = settingsFactory;
            _validator = validator;
        }

        public GameplayJsonSettings Load(GameplaySettingsConfig config)
        {
            if (config == null)
                throw new InvalidOperationException("GameplaySettingsConfig is not assigned.");

            PlayerSettingsJson player = LoadJson<PlayerSettingsJson>(config.PlayerSettingsJson, "player settings");
            EnemiesSettingsJson enemies = LoadJson<EnemiesSettingsJson>(
                config.EnemiesSettingsJson,
                "enemies settings");
            WorldSettingsJson world = LoadJson<WorldSettingsJson>(config.WorldSettingsJson, "world settings");
            GameplayJsonSettings settings = _settingsFactory.Create(player, enemies, world);
            _validator.Validate(settings);

            return settings;
        }

        private T LoadJson<T>(TextAsset textAsset, string label)
        {
            if (textAsset == null)
                throw new InvalidOperationException($"{label} json TextAsset is not assigned.");

            if (string.IsNullOrWhiteSpace(textAsset.text))
                throw new InvalidOperationException($"{label} json is empty.");

            try
            {
                T data = JsonUtility.FromJson<T>(textAsset.text);

                if (data == null)
                    throw new InvalidOperationException($"{label} json cannot be parsed.");

                return data;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"{label} json has invalid format.", exception);
            }
        }
    }
}