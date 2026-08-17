using Zenject;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Gameplay.Time;

namespace AsteroidGame.Scripts.UI.Player.Stats
{
    public sealed class PlayerStatsHudPresenter : IInitializable, ITickable
    {
        private const float UpdateIntervalSeconds = 0.1f;

        private readonly PlayerStatsHudView _view;
        private readonly PlayerStatsHudViewModelFactory _viewModelFactory;
        private readonly PlayerLaserRechargeState _rechargeState;
        private readonly IPlayerStateProvider _playerStateProvider;
        private readonly ITimeProvider _timeProvider;

        private float _remainingUpdateSeconds;

        public PlayerStatsHudPresenter(
            PlayerStatsHudView view,
            PlayerStatsHudViewModelFactory viewModelFactory,
            IPlayerStateProvider playerStateProvider,
            PlayerLaserRechargeState rechargeState,
            ITimeProvider timeProvider)
        {
            _view = view;
            _viewModelFactory = viewModelFactory;
            _playerStateProvider = playerStateProvider;
            _rechargeState = rechargeState;
            _timeProvider = timeProvider;
        }

        void IInitializable.Initialize()
        {
            UpdateView(true);
            _remainingUpdateSeconds = UpdateIntervalSeconds;
        }

        void ITickable.Tick()
        {
            _remainingUpdateSeconds -= _timeProvider.DeltaTime;

            if (_remainingUpdateSeconds > 0f)
                return;

            UpdateView(true);
            _remainingUpdateSeconds = UpdateIntervalSeconds;
        }

        private void UpdateView(bool isVisible)
        {
            PlayerSnapshot snapshot = _playerStateProvider.Snapshot;
            PlayerStatsHudViewModel viewModel = _viewModelFactory.Create(
                snapshot,
                _rechargeState,
                isVisible);

            _view.Render(viewModel);
        }
    }
}