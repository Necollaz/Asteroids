using UnityEngine.Device;
using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Input.Common
{
    public sealed class UnityPlayerInputPlatform : IPlayerInputPlatform
    {
        public bool IsEditor => Application.isEditor;
        public bool IsMobilePlatform => Application.isMobilePlatform;
    }
}