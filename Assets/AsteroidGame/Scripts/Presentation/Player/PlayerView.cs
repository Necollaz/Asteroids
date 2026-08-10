using UnityEngine;
using AsteroidGame.Scripts.Domain.Player;

namespace AsteroidGame.Scripts.Presentation.Player
{
    [DisallowMultipleComponent]
    public sealed class PlayerView : MonoBehaviour
    {
        public void ApplySnapshot(PlayerSnapshot snapshot) => transform.SetPositionAndRotation(
            new Vector3(snapshot.Position.X, snapshot.Position.Y, 0f),
            Quaternion.Euler(0f, 0f, snapshot.RotationDegrees));
    }
}