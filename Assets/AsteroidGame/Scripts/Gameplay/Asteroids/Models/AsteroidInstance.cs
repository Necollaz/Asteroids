using AsteroidGame.Scripts.Domain.Asteroids.Models;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Enemies.Types;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Asteroids.Contracts;

namespace AsteroidGame.Scripts.Gameplay.Asteroids.Models
{
    public sealed class AsteroidInstance
    {
        public AsteroidInstance(AsteroidModel model, CollisionBody collisionBody, IAsteroidView view)
        {
            Model = model;
            CollisionBody = collisionBody;
            View = view;
        }

        public AsteroidModel Model { get; }
        public CollisionBody CollisionBody { get; }
        public Body2D Body => Model.Body;
        public IAsteroidView View { get; }
        public EnemyType Type => Model.Type;

        public void Activate(Vector2D position, Velocity velocity, float rotationDegrees)
        {
            Model.Activate(position, velocity, rotationDegrees);
            CollisionBody.Activate();
            RefreshView();
            View.Show();
        }

        public void Deactivate()
        {
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