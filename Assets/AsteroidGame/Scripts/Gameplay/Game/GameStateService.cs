using System;
using Zenject;
using AsteroidGame.Scripts.Signals.Game;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Gameplay.Game
{
    public sealed class GameStateService : IInitializable, IDisposable, IGameplayPauseState
    {
        private readonly SignalBus _signalBus;

        public GameStateService(SignalBus signalBus) => _signalBus = signalBus;

        public bool IsPaused { get; private set; }

        void IInitializable.Initialize() => _signalBus.Subscribe<PlayerDefeatedSignal>(HandlePlayerDefeated);

        void IDisposable.Dispose() => _signalBus.Unsubscribe<PlayerDefeatedSignal>(HandlePlayerDefeated);

        private void HandlePlayerDefeated()
        {
            if (IsPaused)
                return;

            IsPaused = true;
            
            _signalBus.Fire<GameDefeatStartedSignal>();
        }
    }
}