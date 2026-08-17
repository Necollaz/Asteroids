using Zenject;

namespace AsteroidGame.Scripts.UI.Common
{
    public sealed class UiEventSystemPresenter : IInitializable
    {
        private readonly UiEventSystemView _view;

        public UiEventSystemPresenter(UiEventSystemView view) => _view = view;

        void IInitializable.Initialize() => _view.Show();
    }
}