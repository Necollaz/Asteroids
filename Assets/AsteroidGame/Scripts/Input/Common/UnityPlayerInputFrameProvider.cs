using UnityEngine;
using AsteroidGame.Scripts.Input.Contracts;

namespace AsteroidGame.Scripts.Input.Common
{
    public sealed class UnityPlayerInputFrameProvider : IPlayerInputFrameProvider
    {
        public int CurrentFrame => Time.frameCount;
    }
}