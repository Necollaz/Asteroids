using UnityEngine;
using AsteroidGame.Scripts.Gameplay.Asteroids.Contracts;

namespace AsteroidGame.Scripts.Presentation.Asteroids
{
    [DisallowMultipleComponent]
    public sealed class AsteroidView : MonoBehaviour, IAsteroidView
    {
        [SerializeField] private Transform _rotatingRoot;
        
        public void Show() => gameObject.SetActive(true);
        
        public void Hide() => gameObject.SetActive(false);
        
        public void SetPosition(float x, float y) => transform.position = new Vector3(x, y, transform.position.z);
        
        public void SetRotation(float rotationDegrees) =>
            _rotatingRoot.rotation = Quaternion.Euler(0f, 0f, rotationDegrees);

        private void OnValidate()
        {
            if (_rotatingRoot == null)
                _rotatingRoot = transform;
        }
    }
}