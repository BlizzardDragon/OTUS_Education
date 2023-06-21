using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.Zenject.GameManagers;


namespace FrameworkUnity.Architecture.Zenject.Installers
{
    public class GameSystemsInstaller : MonoInstaller<GameSystemsInstaller>
    {
        public override void InstallBindings()
        {
            InstallGameListeners();
            InstallGameManager();
        }

        private void InstallGameListeners()
        {
            foreach (var gameListener in GetComponentsInChildren<IGameListener>())
            {
                Container.BindInterfacesTo(gameListener.GetType()).FromInstance(gameListener).AsCached();
            }
        }

        private void InstallGameManager()
        {
            Container.Bind<GameManagerContext>().AsSingle();
            Container.Bind<BaseGameManager>().FromComponentInHierarchy().AsSingle();

            // Container.Bind<IGameManager>().To<GameManagerPM>().FromInstance(_gameManager).AsSingle();
            // Container.Bind<IGameManager>().FromInstance(_gameManager).AsSingle();
        }
    }
}
