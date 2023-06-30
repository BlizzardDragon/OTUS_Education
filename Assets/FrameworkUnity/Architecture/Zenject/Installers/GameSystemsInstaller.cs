using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.Zenject.GameManagers;
using FrameworkUnity.Interfaces.Installed;
using PresentationModel;
using System;

namespace FrameworkUnity.Architecture.Zenject.Installers
{
    public class GameSystemsInstaller : MonoInstaller<GameSystemsInstaller>
    {
        public override void InstallBindings()
        {
            InstallGameListeners();
            InstallGameManager();
            InstallGameInstallables();
            InstallGameSystems();
        }

        private void InstallGameSystems()
        {
            Container.Bind<CharacterPopupPresentationModel>().AsSingle();
            Container.Bind<PresentationModel.CharacterInfo>().AsSingle();
            Container.Bind<PlayerLevel>().AsSingle();
            Container.Bind<UserInfo>().AsSingle();
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

        private void InstallGameInstallables()
        {
            foreach (var installable in GetComponentsInChildren<IBootstrapInstallable>())
            {
                Container.BindInterfacesTo(installable.GetType()).FromInstance(installable).AsCached();
            }
        }
    }
}
