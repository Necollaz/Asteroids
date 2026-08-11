using System;
using UnityEngine;
using UnityEngine.UI;

namespace AsteroidGame.Scripts.UI.Player
{
    [Serializable]
    public sealed class PlayerHealthIconView
    {
        [SerializeField] private Image _fullIconImage;

        public void SetFilled(bool isFilled)
        {
            ValidateRequiredReferences();
            _fullIconImage.gameObject.SetActive(isFilled);
        }

        public void ValidateRequiredReferences()
        {
            if (_fullIconImage == null)
                throw new InvalidOperationException($"{nameof(PlayerHealthIconView)} requires full icon image.");
        }
    }
}