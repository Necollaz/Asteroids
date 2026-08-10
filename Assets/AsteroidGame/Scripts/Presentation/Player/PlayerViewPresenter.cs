using Zenject;
using AsteroidGame.Scripts.Gameplay.Player;

namespace AsteroidGame.Scripts.Presentation.Player
{
    public class PlayerViewPresenter: ILateTickable
    {
        private readonly IPlayerStateProvider _playerStateProvider;
        private readonly PlayerView _playerView;

        public PlayerViewPresenter(IPlayerStateProvider playerStateProvider, PlayerView playerView)
        {
            _playerStateProvider = playerStateProvider;
            _playerView = playerView;
        }

        public void LateTick() => _playerView.ApplySnapshot(_playerStateProvider.Snapshot);
    }
}