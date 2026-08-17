using Zenject;
using AsteroidGame.Scripts.Input.Common;
using AsteroidGame.Scripts.Input.Mobile.Views;

namespace AsteroidGame.Scripts.Input.Mobile.Presenters
{
    public sealed class MobileInputVisibilityPresenter : IInitializable
    {
        private readonly PlayerInputSourceResolver _sourceResolver;
        private readonly MobilePlayerInputView _view;

        public MobileInputVisibilityPresenter(PlayerInputSourceResolver sourceResolver, MobilePlayerInputView view)
        {
            _sourceResolver = sourceResolver;
            _view = view;
        }

        void IInitializable.Initialize()
        {
            if (_sourceResolver.Resolve() == PlayerInputSourceType.Mobile)
            {
                _view.Show();
                
                return;
            }
            
            _view.Hide();
        }
    }
}