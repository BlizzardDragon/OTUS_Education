using UnityEngine;
using System.Collections.Generic;
using FrameworkUnity.Architecture.DI;
using FrameworkUnity.Architecture.Zenject.GameManagers;
using FrameworkUnity.Architecture.Locators;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Interfaces.Installed;


namespace FrameworkUnity.Architecture.Zenject.Installers
{
    [RequireComponent(typeof(ServiceLocatorInstaller), typeof(DependencyResolver))]
    public class BaseBootstrapInstaller : MonoBehaviour
    {
        private List<IInstallableOnAwake> _awakeInstallables = new();
        private List<IInstallableOnStart> _startInstallables = new();
        private List<IInstallableOnEnable> _enableInstallables = new();
        private List<IInstallableOnDisable> _disableInstallables = new();
        protected BaseGameManager _gameManager;


        protected virtual void Awake()
        {
            InstallServices();
            SetGameManager();
            InstallGameManager();
            ResolveDependencies();
            FindInstallables();

            foreach (var installable in _awakeInstallables)
            {
                installable.InstallOnAwake();
            }
        }

        protected virtual void Start()
        {
            foreach (var installable in _startInstallables)
            {
                installable.InstallOnStart();
            }
        }

        protected virtual void OnEnable()
        {
            foreach (var installable in _enableInstallables)
            {
                installable.InstallOnEnable();
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var installable in _disableInstallables)
            {
                installable.InstallOnDisable();
            }
        }

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

        private void FindInstallables()
        {
            IInstallable[] installables = GetComponentsInChildren<IInstallable>();
            foreach (var installable in installables)
            {
                if (installable is IInstallableOnAwake installableOnAwake)
                {
                    _awakeInstallables.Add(installableOnAwake);
                }
                if (installable is IInstallableOnStart installableOnStart)
                {
                    _startInstallables.Add(installableOnStart);
                }
                if (installable is IInstallableOnEnable installableOnEnable)
                {
                    _enableInstallables.Add(installableOnEnable);
                }
                if (installable is IInstallableOnDisable installableOnDisable)
                {
                    _disableInstallables.Add(installableOnDisable);
                }
            }
        }
    }
}