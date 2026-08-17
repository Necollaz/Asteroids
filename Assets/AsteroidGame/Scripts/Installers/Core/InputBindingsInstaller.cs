using Zenject;
using AsteroidGame.Scripts.Input;
using AsteroidGame.Scripts.Input.Common;
using AsteroidGame.Scripts.Input.Contracts;
using AsteroidGame.Scripts.Input.Keyboard;
using AsteroidGame.Scripts.Input.Mobile;
using AsteroidGame.Scripts.Input.Mobile.Presenters;
using AsteroidGame.Scripts.Input.Mobile.Views;

namespace AsteroidGame.Scripts.Installers.Core
{
    public sealed class InputBindingsInstaller : Installer<InputBindingsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IKeyboardInputReader>().To<UnityKeyboardInputReader>().AsSingle();
            Container.Bind<KeyboardPlayerInput>().AsSingle();
            Container.Bind<MobilePlayerInputView>().FromComponentInHierarchy().AsSingle();
            Container.Bind<MobilePlayerInput>().AsSingle();
            Container.Bind<IPlayerInputPlatform>().To<UnityPlayerInputPlatform>().AsSingle();
            Container.Bind<IPlayerInputFrameProvider>().To<UnityPlayerInputFrameProvider>().AsSingle();
            Container.Bind<PlayerInputSourceResolver>().AsSingle();
            Container.Bind<IPlayerInput>().To<PlayerInputRouter>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<MobileInputVisibilityPresenter>().AsSingle();
        }
    }
}