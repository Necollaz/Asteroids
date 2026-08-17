using UnityEngine;

namespace AsteroidGame.Scripts.Input.Contracts
{
    public interface IKeyboardInputSettingsData
    {
        KeyCode TurnLeftKey { get; }
        KeyCode TurnRightKey { get; }
        KeyCode ThrustKey { get; }
        KeyCode FireBulletKey { get; }
        KeyCode FireLaserKey { get; }
        KeyCode AlternativeTurnLeftKey { get; }
        KeyCode AlternativeTurnRightKey { get; }
        KeyCode AlternativeThrustKey { get; }
        KeyCode AlternativeFireBulletKey { get; }
        KeyCode AlternativeFireLaserKey { get; }
    }
}