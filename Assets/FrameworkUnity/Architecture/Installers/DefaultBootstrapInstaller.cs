using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Architecture.Locators;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using UnityEngine;


namespace FrameworkUnity.Architecture.Installers
{
    [RequireComponent(typeof(ServiceLocatorInstaller), typeof(DependencyResolver))]
    public class DefaultBootstrapInstaller : MonoBehaviour
    {
        protected virtual void Awake()
        {
            InstallServices();
            InstallGameManager();
            ResolveDependencies();
        }

        protected virtual void Start() { }

        protected virtual void OnEnable()
        {
            var gameManager = ServiceLocator.GetService<DefaultGameManager>();
        }

        protected virtual void OnDisable()
        {
            var gameManager = ServiceLocator.GetService<DefaultGameManager>();
        }

        protected virtual void OnDestroy()
        {
            ServiceLocator.ClearServices();
        }



        private void InstallServices() => GetComponent<ServiceLocatorInstaller>().InstallServices();
        private void ResolveDependencies() => GetComponent<DependencyResolver>().ResolveDependencies();

        private void InstallGameManager()
        {
            IGameListener[] listeners = GetComponentsInChildren<IGameListener>();
            foreach (var listener in listeners)
            {
                ServiceLocator.GetService<DefaultGameManager>().AddListener(listener);
            }
        }
    }
}