using System;
using UnityEngine;

namespace AsteroidGame.Scripts.Infrastructure.Configs.Json.Loading
{
    public sealed class KeyCodeParser
    {
        public KeyCode Parse(string value, string fieldPath)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidOperationException($"{fieldPath} must be assigned in json.");

            if (!Enum.TryParse(value, out KeyCode keyCode))
                throw new InvalidOperationException($"{fieldPath} has invalid KeyCode value '{value}'.");

            if (keyCode == KeyCode.None)
                throw new InvalidOperationException($"{fieldPath} cannot be KeyCode.None.");

            return keyCode;
        }
    }
}