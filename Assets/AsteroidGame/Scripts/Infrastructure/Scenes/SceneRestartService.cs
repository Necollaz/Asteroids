using System;
using UnityEngine.SceneManagement;
using Zenject;
using AsteroidGame.Scripts.Signals.Game;

namespace AsteroidGame.Scripts.Infrastructure.Scenes
{
    public sealed class SceneRestartService : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        public SceneRestartService(SignalBus signalBus) => _signalBus = signalBus;

        void IInitializable.Initialize() => _signalBus.Subscribe<GameRestartRequestedSignal>(RestartActiveScene);

        void IDisposable.Dispose() => _signalBus.Unsubscribe<GameRestartRequestedSignal>(RestartActiveScene);

        private void RestartActiveScene()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }
    }
}