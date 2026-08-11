using Zenject;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;
using AsteroidGame.Scripts.Presentation.Player.Views;

namespace AsteroidGame.Scripts.Presentation.Player.Presenters
{
    public sealed class PlayerViewPresenter : ILateTickable
    {
        private readonly IPlayerStateProvider _playerStateProvider;
        private readonly PlayerView _playerView;

        public PlayerViewPresenter(IPlayerStateProvider playerStateProvider, PlayerView playerView)
        {
            _playerStateProvider = playerStateProvider;
            _playerView = playerView;
        }

        void ILateTickable.LateTick() => _playerView.ApplySnapshot(_playerStateProvider.Snapshot);
    }
}