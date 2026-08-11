using UnityEngine;

namespace AsteroidGame.Scripts.Input
{
    public interface IKeyboardInputSettingsData
    {
        KeyCode TurnLeftKey { get; }
        KeyCode TurnRightKey { get; }
        KeyCode ThrustKey { get; }
        KeyCode FireBulletKey { get; }
        KeyCode AlternativeTurnLeftKey { get; }
        KeyCode AlternativeTurnRightKey { get; }
        KeyCode AlternativeThrustKey { get; }
        KeyCode AlternativeFireBulletKey { get; }
    }
}