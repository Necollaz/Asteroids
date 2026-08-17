using System;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Player.Factories;
using AsteroidGame.Scripts.Domain.Player.Models;
using AsteroidGame.Scripts.Gameplay.Player.Contracts;

namespace AsteroidGame.Scripts.Gameplay.Player.Facades
{
    public sealed class PlayerFacade : IPlayerBodyProvider, IPlayerStateProvider, IPlayerControlState
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerSnapshotFactory _snapshotFactory;

        public PlayerFacade(PlayerModel playerModel, PlayerSnapshotFactory snapshotFactory)
        {
            _playerModel = playerModel ?? throw new ArgumentNullException(nameof(playerModel));
            _snapshotFactory = snapshotFactory ?? throw new ArgumentNullException(nameof(snapshotFactory));
        }
        
        public Body2D Body => _playerModel.Body;
        public PlayerSnapshot Snapshot => _snapshotFactory.Create(_playerModel);
        public bool CanControl => _playerModel.CanControl;
    }
}