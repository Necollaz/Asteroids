using System;
using Zenject;
using AsteroidGame.Scripts.Signals.Game;

namespace AsteroidGame.Scripts.UI.Common
{
    public sealed class UiEventSystemPresenter : IInitializable, IDisposable
    {
        private readonly UiEventSystemView _view;
        private readonly SignalBus _signalBus;

        public UiEventSystemPresenter(UiEventSystemView view, SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<GameDefeatStartedSignal>(EnableUiInput);
            _view.Hide();
        }

        void IDisposable.Dispose() => _signalBus.Unsubscribe<GameDefeatStartedSignal>(EnableUiInput);

        private void EnableUiInput() => _view.Show();
    }
}