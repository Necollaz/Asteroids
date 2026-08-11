using System;
using Zenject;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.UI.Player
{
    public sealed class PlayerLaserHudPresenter : IInitializable, IDisposable, ITickable
    {
        private readonly PlayerLaserHudView _view;
        private readonly PlayerLaserMagazine _magazine;
        private readonly PlayerLaserRechargeState _rechargeState;
        private readonly SignalBus _signalBus;

        private int _lastRechargeTenths = -1;

        public PlayerLaserHudPresenter(
            PlayerLaserHudView view,
            PlayerLaserMagazine magazine,
            PlayerLaserRechargeState rechargeState,
            SignalBus signalBus)
        {
            _view = view;
            _magazine = magazine;
            _rechargeState = rechargeState;
            _signalBus = signalBus;
        }

        void IInitializable.Initialize()
        {
            _signalBus.Subscribe<PlayerLaserChargesChangedSignal>(HandleChargesChanged);

            _view.SetCharges(_magazine.Charges, _magazine.MaxCharges);
            UpdateRechargeText();
        }

        void IDisposable.Dispose() => _signalBus.Unsubscribe<PlayerLaserChargesChangedSignal>(HandleChargesChanged);

        void ITickable.Tick() => UpdateRechargeText();

        private void HandleChargesChanged(PlayerLaserChargesChangedSignal signal) =>
            _view.SetCharges(signal.Charges, signal.MaxCharges);

        private void UpdateRechargeText()
        {
            int rechargeTenths = _rechargeState.IsRecharging 
                ? (int)Math.Ceiling(_rechargeState.RemainingSeconds * 10f)
                : 0;

            if (_lastRechargeTenths == rechargeTenths)
                return;

            _lastRechargeTenths = rechargeTenths;
            _view.SetRechargeTenths(rechargeTenths);
        }
    }
}