using System;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;

namespace AsteroidGame.Scripts.Gameplay.Player.Services
{
    public sealed class PlayerControlStateProvider : IPlayerControlState
    {
        private readonly PlayerModel _playerModel;
        
        public PlayerControlStateProvider(PlayerModel playerModel) =>
            _playerModel = playerModel ?? throw new ArgumentNullException(nameof(playerModel));

        public bool CanControl => _playerModel.CanControl;
    }
}