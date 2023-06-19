using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using FrameworkUnity.Interfaces.Listeners.GameListeners;
using FrameworkUnity.Architecture.Zenject.GameManagers;


namespace FrameworkUnity.Architecture.Zenject.Installers
{
    public class GameListenerInstaller : MonoInstaller<GameListenerInstaller>
    {
        [Inject]
        private readonly BaseGameManager _gameManager;


        public override void InstallBindings()
        {
            IGameListener[] gameListeners = GetComponentsInChildren<IGameListener>();
            foreach (var gameListener in gameListeners)
            {
                Container.Bind<IGameListener>().FromInstance(gameListener).AsCached();
            }
        }
    }
}
