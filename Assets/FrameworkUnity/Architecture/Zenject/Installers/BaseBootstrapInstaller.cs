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
        private List<IInstallableOnDisable> _disableInstallables = new();
        protected BaseGameManager _gameManager;
        protected DiContainer _diContainer;


        [Inject]
        private void Construct(BaseGameManager gameManager, DiContainer diContainer)
        {
            _gameManager = gameManager;
            _diContainer = diContainer;
        }

        protected virtual void Awake()
        {
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

        // protected virtual void OnDestroy() => ServiceLocator.ClearServices();

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