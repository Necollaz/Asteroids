using System;
using Zenject;
using AsteroidGame.Scripts.Signals.Game;

namespace AsteroidGame.Scripts.UI.Game
{
    public sealed class DefeatGamePresenter : IInitializable, IDisposable
    {
        private readonly DefeatGameView _view;
        private readonly SignalBus _signalBus;

        public DefeatGamePresenter(DefeatGameView view, SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<GameDefeatStartedSignal>(ShowDefeat);
            _view.RestartClicked += RequestRestart;
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<GameDefeatStartedSignal>(ShowDefeat);
            _view.RestartClicked -= RequestRestart;
        }

        private void ShowDefeat() => _view.Show();

        private void RequestRestart() => _signalBus.Fire<GameRestartRequestedSignal>();
    }
}