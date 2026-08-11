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
        private readonly SignalBus _signalBus;
        private readonly ITimeProvider _timeProvider;

        private float _remainingVisibleSeconds;

        public PlayerHealthPresenter(
            PlayerHealthView view,
            PlayerHealthState health,
            ITimeProvider timeProvider,
            SignalBus signalBus)
        {
            _view = view;
            _health = health;
            _timeProvider = timeProvider;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<PlayerDamagedSignal>(HandlePlayerDamaged);

            _view.SetHealth(_health.CurrentHealth, _health.MaxHealth);
            _view.Show();
            _remainingVisibleSeconds = _view.InitialVisibleSeconds;
        }

        void IDisposable.Dispose() => _signalBus.Unsubscribe<PlayerDamagedSignal>(HandlePlayerDamaged);

        void ITickable.Tick()
        {
            if (_remainingVisibleSeconds <= 0f)
                return;

            _remainingVisibleSeconds -= _timeProvider.DeltaTime;

            if (_remainingVisibleSeconds <= 0f)
                _view.Hide();
        }

        private void HandlePlayerDamaged(PlayerDamagedSignal signal)
        {
            _view.SetHealth(signal.CurrentHealth, signal.MaxHealth);
            _view.Show();
            _remainingVisibleSeconds = _view.DamageVisibleSeconds;
        }
    }
}