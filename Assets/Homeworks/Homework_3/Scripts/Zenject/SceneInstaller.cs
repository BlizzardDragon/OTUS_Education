using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using FrameworkUnity.Interfaces.Installed;
using FrameworkUnity.Architecture.GameManagers;


namespace FrameworkUnity.Architecture.Zenject
{
    public class SceneInstaller : MonoInstaller<SceneInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameManager>().FromComponentInHierarchy().AsSingle();

            // Container.Bind<IGameManager>().To<GameManagerPM>().FromInstance(_gameManager).AsSingle();
            // Container.Bind<IGameManager>().FromInstance(_gameManager).AsSingle();
        }
    }
}
