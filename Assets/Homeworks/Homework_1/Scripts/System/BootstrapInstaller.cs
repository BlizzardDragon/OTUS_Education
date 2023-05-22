using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ServiceLocatorInstaller))]
public sealed class BootstrapInstaller : MonoBehaviour
{
    private void Awake()
    {
        InstallServices();
        InstallGameManager();
        ServiceLocator.GetService<IRoadTarget>().InstallTarget();
    }
    
    private void OnEnable()
    {
        ServiceLocator.GetService<CountdownText>().OnGameStarted += ServiceLocator.GetService<GameManager>().StartGame;
    }

    private void OnDisable()
    {
        ServiceLocator.GetService<CountdownText>().OnGameStarted -= ServiceLocator.GetService<GameManager>().StartGame;
    }

    private void InstallServices() => GetComponent<ServiceLocatorInstaller>().InstallServices();

    private void InstallGameManager()
    {
        IGameListener[] listeners = GetComponentsInChildren<IGameListener>();
        foreach (var listener in listeners)
        {
            ServiceLocator.GetService<GameManager>().AddListener(listener);
        }
    }
}





//// Variation.
// public sealed class GameListenersInstaller : MonoBehaviour
// {
//     [SerializeField] private MonoBehaviour[] _listeners;

//     private void Awake()
//     {
//         var gameManager = FindObjectOfType<GameManager>();

//         foreach (var listener in _listeners)
//         {
//             if (listener is IGameListener gameListener)
//             {
//                 gameManager.AddListener(gameListener);
//             }
//         }
//     }
// }
