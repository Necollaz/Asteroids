using System;
using UnityEngine;
using AsteroidGame.Scripts.Gameplay.Ufo.Contracts;

namespace AsteroidGame.Scripts.Presentation.Ufo
{
    [DisallowMultipleComponent]
    public class UfoView : MonoBehaviour, IUfoView
    {
        [SerializeField] private Transform _rotatingRoot;

        private void Awake() => ValidateRequiredReferences();

        private void OnValidate()
        {
            if (_rotatingRoot == null)
                _rotatingRoot = transform;
        }
        
        public void Show() => gameObject.SetActive(true);
        
        public void Hide() => gameObject.SetActive(false);
        
        public void SetPosition(float x, float y) => transform.position = new Vector3(x, y, transform.position.z);
        
        public void SetRotation(float rotationDegrees) =>
            _rotatingRoot.rotation = Quaternion.Euler(0f, 0f, rotationDegrees);

        private void ValidateRequiredReferences()
        {
            if (_rotatingRoot == null)
                throw new InvalidOperationException($"{nameof(UfoView)} requires rotating root.");
        }
    }
}