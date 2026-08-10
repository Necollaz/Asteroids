using AsteroidGame.Scripts.Domain.Player;

namespace AsteroidGame.Scripts.Gameplay.Player.Services
{
    public sealed class PlayerDamageService
    {
        private readonly PlayerHealthService _healthService;
        private readonly PlayerInvulnerabilityService _invulnerabilityService;
        private readonly PlayerCollisionSettings _settings;

        public PlayerDamageService(
            PlayerHealthService healthService,
            PlayerInvulnerabilityService invulnerabilityService,
            PlayerCollisionSettings settings)
        {
            _healthService = healthService;
            _invulnerabilityService = invulnerabilityService;
            _settings = settings;
        }
        
        public bool CanReceiveCollisionDamage => !_invulnerabilityService.IsInvulnerable;

        public void ApplyCollisionDamage()
        {
            if (!CanReceiveCollisionDamage)
                return;
            
            _healthService.ApplyDamage(1);
            _invulnerabilityService.StartInvulnerability(_settings.InvulnerabilitySeconds);
        }
    }
}