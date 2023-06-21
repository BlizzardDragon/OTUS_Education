using UnityEngine;
using System.Collections.Generic;
using FrameworkUnity.Architecture.Zenject.GameManagers;
using FrameworkUnity.Interfaces.Installed;
using Zenject;


namespace FrameworkUnity.Architecture.Zenject.Installers
{
    public class BaseBootstrapInstaller : MonoBehaviour
    {
        private List<IInstallableOnAwake> _awakeInstallables = new();
        private List<IInstallableOnStart> _startInstallables = new();
        private List<IInstallableOnEnable> _enableInstallables = new();
        private List<IUninstallableOnDisable> _disableInstallables = new();
        protected BaseGameManager _gameManager;
        protected DiContainer _container;


        [Inject]
        private void Construct(BaseGameManager gameManager, DiContainer diContainer)
        {
            _gameManager = gameManager;
            _container = diContainer;
        }

        protected virtual void Awake()
        {
            foreach (var installable in _container.Resolve<IEnumerable<IInstallableOnAwake>>())
            {
                installable.InstallOnAwake();
            }
        }

        protected virtual void Start()
        {
            foreach (var installable in _container.Resolve<IEnumerable<IInstallableOnStart>>())
            {
                installable.InstallOnStart();
            }
        }

        protected virtual void OnEnable()
        {
            foreach (var installable in _container.Resolve<IEnumerable<IInstallableOnEnable>>())
            {
                installable.InstallOnEnable();
            }
        }

        protected virtual void OnDisable()
        {
            foreach (var installable in _container.Resolve<IEnumerable<IUninstallableOnDisable>>())
            {
                installable.UninstallOnDisable();
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var installable in _container.Resolve<IEnumerable<IUninstallableOnDestroy>>())
            {
                installable.UninstallOnDestroy();
            }
        }

        // protected virtual void OnDestroy() => ServiceLocator.ClearServices();
    }
}