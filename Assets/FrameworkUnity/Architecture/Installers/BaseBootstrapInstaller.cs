using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Architecture.GameManagers;
using FrameworkUnity.Architecture.Locators;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using UnityEngine;


namespace FrameworkUnity.Architecture.Installers
{
    [RequireComponent(typeof(ServiceLocatorInstaller), typeof(DependencyResolver))]
    public class BaseBootstrapInstaller : MonoBehaviour
    {
        protected BaseGameManager _gameManager;


        protected virtual void Awake()
        {
            InstallServices();
            SetGameManager();
            InstallGameManager();
            ResolveDependencies();
        }

        protected virtual void Start() { }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable() { }
        protected virtual void OnDestroy() => ServiceLocator.ClearServices();


        private void InstallServices() => GetComponent<ServiceLocatorInstaller>().InstallServices();
        private void SetGameManager() => _gameManager = ServiceLocator.GetService<BaseGameManager>();

        private void InstallGameManager()
        {
            IGameListener[] listeners = GetComponentsInChildren<IGameListener>();
            foreach (var listener in listeners)
            {
                _gameManager.AddListener(listener);
            }
        }

        private void ResolveDependencies() => GetComponent<DependencyResolver>().ResolveDependencies();
    }
}