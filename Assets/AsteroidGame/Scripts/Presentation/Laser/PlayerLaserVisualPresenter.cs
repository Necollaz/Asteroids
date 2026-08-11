using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Laser.Contracts;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Presentation.Laser
{
    public sealed class PlayerLaserVisualPresenter : IInitializable, IDisposable
    {
        private readonly PlayerLaserView _view;
        private readonly ILaserSpawnPointProvider _spawnPointProvider;
        private readonly IGameplayPauseState _gameplayPauseState;
        private readonly ITimeProvider _timeProvider;
        private readonly PlayerLaserSettings _laserSettings;
        private readonly SignalBus _signalBus;

        private CancellationTokenSource _lifetimeCancellation;
        private CancellationTokenSource _showCancellation;

        public PlayerLaserVisualPresenter(
            PlayerLaserView view,
            ILaserSpawnPointProvider spawnPointProvider,
            IGameplayPauseState gameplayPauseState,
            ITimeProvider timeProvider,
            PlayerLaserSettings laserSettings,
            SignalBus signalBus)
        {
            _view = view;
            _spawnPointProvider = spawnPointProvider;
            _gameplayPauseState = gameplayPauseState;
            _timeProvider = timeProvider;
            _laserSettings = laserSettings;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _lifetimeCancellation = new CancellationTokenSource();
            _signalBus.Subscribe<PlayerLaserFiredSignal>(HandleLaserFired);
            _view.Hide();
        }

        void IDisposable.Dispose()
        {
            _signalBus.Unsubscribe<PlayerLaserFiredSignal>(HandleLaserFired);

            _showCancellation?.Cancel();
            _showCancellation?.Dispose();
            _showCancellation = null;

            _lifetimeCancellation?.Cancel();
            _lifetimeCancellation?.Dispose();
            _lifetimeCancellation = null;
        }

        private void HandleLaserFired(PlayerLaserFiredSignal signal)
        {
            _showCancellation?.Cancel();
            _showCancellation?.Dispose();
            _showCancellation = CancellationTokenSource.CreateLinkedTokenSource(_lifetimeCancellation.Token);

            ShowLaserAsync(signal, _showCancellation.Token).Forget();
        }

        private async UniTask ShowLaserAsync(PlayerLaserFiredSignal signal, CancellationToken cancellationToken)
        {
            float remainingSeconds = signal.VisibleSeconds;

            try
            {
                while (remainingSeconds > 0f)
                {
                    if (!_gameplayPauseState.IsPaused)
                    {
                        RefreshLaser(signal.VisualWidth);
                        
                        remainingSeconds -= _timeProvider.DeltaTime;
                    }

                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                }

                _view.Hide();
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void RefreshLaser(float visualWidth)
        {
            Vector2D startPosition = _spawnPointProvider.Position;
            Vector2D direction = _spawnPointProvider.Direction;
            Vector2D endPosition = startPosition.Add(direction.Multiply(_laserSettings.Length));

            _view.Show(startPosition, endPosition, visualWidth);
        }
    }
}