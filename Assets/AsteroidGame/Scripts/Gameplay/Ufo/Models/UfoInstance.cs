using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Domain.Ufo.Models;
using AsteroidGame.Scripts.Gameplay.Ufo.Contracts;
using AsteroidGame.Scripts.Gameplay.Ufo.States;

namespace AsteroidGame.Scripts.Gameplay.Ufo.Models
{
    public sealed class UfoInstance
    {
        public UfoInstance(UfoModel model, CollisionBody collisionBody, IUfoView view, UfoKnockbackState knockbackState)
        {
            Model = model;
            CollisionBody = collisionBody;
            View = view;
            KnockbackState = knockbackState;
        }
        
        public UfoModel Model { get; }
        public CollisionBody CollisionBody { get; }
        public IUfoView View { get; }
        public UfoKnockbackState KnockbackState { get; }
        public Body2D Body => Model.Body;
        public EnemyType Type => Model.Type;

        public void Activate(Vector2D position, Velocity velocity, float rotationDegrees)
        {
            KnockbackState.Reset();
            Model.Activate(position, velocity, rotationDegrees);
            CollisionBody.Activate();
            RefreshView();
            View.Show();
        }

        public void Deactivate()
        {
            KnockbackState.Reset();
            Model.Deactivate();
            CollisionBody.Deactivate();
            View.Hide();
        }

        public void RefreshView()
        {
            View.SetPosition(Body.Position.X, Body.Position.Y);
            View.SetRotation(Body.RotationDegrees);
        }
    }
}