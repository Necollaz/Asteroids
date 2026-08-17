using System;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Domain.Player.States;

namespace AsteroidGame.Scripts.UI.Player.Stats
{
    public sealed class PlayerStatsHudViewModelFactory
    {
        private readonly PlayerStatsHudTextCache _textCache;

        public PlayerStatsHudViewModelFactory(PlayerStatsHudTextCache textCache) => 
            _textCache = textCache ?? throw new ArgumentNullException(nameof(textCache));

        public PlayerStatsHudViewModel Create(
            PlayerSnapshot snapshot,
            PlayerLaserRechargeState rechargeState,
            bool isVisible)
        {
            _textCache.Update(snapshot, rechargeState);

            return new PlayerStatsHudViewModel(
                isVisible,
                _textCache.PositionText,
                _textCache.RotationText,
                _textCache.SpeedText,
                _textCache.LaserChargesText,
                _textCache.LaserCooldownText);
        }
    }
}