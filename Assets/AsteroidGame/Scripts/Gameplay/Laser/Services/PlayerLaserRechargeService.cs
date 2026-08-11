using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Gameplay.Game;
using AsteroidGame.Scripts.Gameplay.Time;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Gameplay.Laser.Services
{
   public sealed class PlayerLaserRechargeService : IInitializable, IDisposable
    {
        private readonly PlayerLaserSettings _settings;
        private readonly PlayerLaserMagazine _magazine;
        private readonly PlayerLaserRechargeState _rechargeState;
        private readonly IGameplayPauseState _pauseState;
        private readonly ITimeProvider _timeProvider;
        private readonly SignalBus _signalBus;

        private CancellationTokenSource _cancellationTokenSource;

        public PlayerLaserRechargeService(
            PlayerLaserSettings settings,
            PlayerLaserMagazine magazine,
            PlayerLaserRechargeState rechargeState,
            IGameplayPauseState pauseState,
            ITimeProvider timeProvider,
            SignalBus signalBus)
        {
            _settings = settings;
            _magazine = magazine;
            _rechargeState = rechargeState;
            _pauseState = pauseState;
            _timeProvider = timeProvider;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            RechargeLoopAsync(_cancellationTokenSource.Token).Forget();
        }

        void IDisposable.Dispose()
        {
            if (_cancellationTokenSource == null)
                return;

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = null;
        }

        private async UniTask RechargeLoopAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);

                    if (_pauseState.IsPaused)
                        continue;

                    if (_magazine.IsFull)
                    {
                        _rechargeState.Stop();

                        continue;
                    }

                    if (!_rechargeState.IsRecharging)
                        _rechargeState.Start(_settings.RechargeSeconds);

                    _rechargeState.Tick(_timeProvider.DeltaTime);

                    if (_rechargeState.RemainingSeconds > 0f)
                        continue;

                    if (_magazine.TryAddCharge())
                        _signalBus.Fire(new PlayerLaserChargesChangedSignal(_magazine.Charges, _magazine.MaxCharges));

                    _rechargeState.Stop();
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}