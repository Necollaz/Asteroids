using AsteroidGame.Scripts.Domain.Bullets.Models;
using AsteroidGame.Scripts.Domain.Collision.Bodies;
using AsteroidGame.Scripts.Domain.Physics.Models;
using AsteroidGame.Scripts.Gameplay.Bullets.Contracts;

namespace AsteroidGame.Scripts.Gameplay.Bullets.Models
{
    public sealed class BulletInstance
    {
        public BulletInstance(BulletModel model, CollisionBody collisionBody, IBulletView view)
        {
            Model = model;
            CollisionBody = collisionBody;
            View = view;
        }

        public BulletModel Model { get; }
        public CollisionBody CollisionBody { get; }
        public IBulletView View { get; }

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
            BulletSnapshot snapshot = Model.CreateSnapshot();

            View.SetPosition(snapshot.Position.X, snapshot.Position.Y);
            View.SetRotation(snapshot.RotationDegrees);
        }
    }
}