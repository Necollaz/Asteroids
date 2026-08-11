using Zenject;
using AsteroidGame.Scripts.Domain.Player.Settings;
using AsteroidGame.Scripts.Domain.Player.States;
using AsteroidGame.Scripts.Signals.Player;

namespace AsteroidGame.Scripts.Gameplay.Player.Services
{
    public sealed class PlayerDamageService
    {
        private const int CollisionDamage = 1;

        private readonly PlayerHealthState _health;
        private readonly PlayerInvulnerabilityState _invulnerability;
        private readonly PlayerCollisionSettings _settings;
        private readonly SignalBus _signalBus;

        public PlayerDamageService(
            PlayerHealthState health,
            PlayerInvulnerabilityState invulnerability,
            PlayerCollisionSettings settings,
            SignalBus signalBus)
        {
            _health = health;
            _invulnerability = invulnerability;
            _settings = settings;
            _signalBus = signalBus;
        }

        public bool CanReceiveCollisionDamage => !_health.IsDead && !_invulnerability.IsActive;

        public void ApplyCollisionDamage()
        {
            if (!CanReceiveCollisionDamage)
                return;

            if (!_health.ApplyDamage(CollisionDamage))
                return;

            _signalBus.Fire(new PlayerDamagedSignal(_health.CurrentHealth, _health.MaxHealth));

            if (_health.IsDead)
            {
                _signalBus.Fire<PlayerDefeatedSignal>();
                return;
            }

            _invulnerability.Start(_settings.InvulnerabilitySeconds);
            _signalBus.Fire<PlayerInvulnerabilityStartedSignal>();
        }
    }
}