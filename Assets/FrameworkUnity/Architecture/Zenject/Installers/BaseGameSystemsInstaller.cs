using Zenject;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.Zenject.GameManagers;
using FrameworkUnity.Interfaces.Installed;


namespace FrameworkUnity.Architecture.Zenject.Installers
{
    public class BaseGameSystemsInstaller : MonoInstaller<BaseGameSystemsInstaller>
    {
        public override void InstallBindings()
        {
            InstallGameListeners();
            InstallGameManager();
            InstallGameInstallables();
            InstallGameSystems();
        }

        protected virtual void InstallGameSystems() { }

        private void InstallGameListeners()
        {
            foreach (var gameListener in GetComponentsInChildren<IGameListener>(true))
            {
                Container.BindInterfacesTo(gameListener.GetType()).FromInstance(gameListener).AsCached();
            }
        }

        private void InstallGameManager()
        {
            Container.Bind<GameManagerContext>().AsSingle();
            Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();

            // Примеры биндинга.
            // Container.Bind<IGameManager>().To<GameManagerPM>().FromInstance(_gameManager).AsSingle();
            // Container.Bind<IGameManager>().FromInstance(_gameManager).AsSingle();
        }

        private void InstallGameInstallables()
        {
            foreach (var installable in GetComponentsInChildren<IBootstrapInstallable>(true))
            {
                Container.BindInterfacesTo(installable.GetType()).FromInstance(installable).AsCached();
            }
        }
    }
}
