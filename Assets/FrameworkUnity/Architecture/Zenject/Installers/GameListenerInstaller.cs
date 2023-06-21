using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using FrameworkUnity.Interfaces.Listeners.GameListeners;


namespace FrameworkUnity.Architecture.Zenject.Installers
{
    public class GameListenerInstaller : MonoInstaller<GameListenerInstaller>
    {
        public override void InstallBindings()
        {
            foreach (var gameListener in GetComponentsInChildren<IGameListener>())
            {
                Container.BindInterfacesTo(gameListener.GetType()).FromInstance(gameListener).AsCached();
            }
        }
    }
}
