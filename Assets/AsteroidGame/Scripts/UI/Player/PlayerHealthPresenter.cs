using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.UI.Player
{
    public sealed class PlayerHealthPresenter : IInitializable, IDisposable, ITickable
    {
        private readonly PlayerHealthView _view;
        private readonly PlayerHealthState _health;
        private readonly PlayerHealthHudSettings _settings;
        private readonly PlayerHealthViewModelFactory _viewModelFactory;
        private readonly SignalBus _signalBus;
        private readonly ITimeProvider _timeProvider;

        private float _remainingVisibleSeconds;

        public PlayerHealthPresenter(
            PlayerHealthView view,
            PlayerHealthState health,
            PlayerHealthHudSettings settings,
            PlayerHealthViewModelFactory viewModelFactory,
            ITimeProvider timeProvider,
            SignalBus signalBus)
        {
            _view = view;
            _health = health;
            _settings = settings;
            _viewModelFactory = viewModelFactory;
            _timeProvider = timeProvider;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<PlayerDamagedSignal>(HandlePlayerDamaged);

            _remainingVisibleSeconds = _settings.InitialVisibleSeconds;
            RenderHealth(true);
        }

        void IDisposable.Dispose() => _signalBus.Unsubscribe<PlayerDamagedSignal>(HandlePlayerDamaged);

        void ITickable.Tick()
        {
            if (_remainingVisibleSeconds <= 0f)
                return;

            _remainingVisibleSeconds -= _timeProvider.DeltaTime;

            if (_remainingVisibleSeconds <= 0f)
                RenderHealth(false);
        }

        private void HandlePlayerDamaged(PlayerDamagedSignal signal)
        {
            _remainingVisibleSeconds = _settings.DamageVisibleSeconds;
            PlayerHealthViewModel viewModel = _viewModelFactory.Create(
                signal.CurrentHealth,
                signal.MaxHealth,
                true);
            _view.Render(viewModel);
        }

        private void RenderHealth(bool isVisible)
        {
            PlayerHealthViewModel viewModel = _viewModelFactory.Create(
                _health.CurrentHealth, 
                _health.MaxHealth,
                isVisible);
            _view.Render(viewModel);
        }
    }
}