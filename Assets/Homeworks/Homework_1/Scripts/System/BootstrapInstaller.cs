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
        var gameManager = ServiceLocator.GetService<GameManager>();

        ServiceLocator.GetService<CountdownText>().OnGameStarted += gameManager.StartGame;
        ServiceLocator.GetService<CountdownText>().OnGameResumed += gameManager.ResumeGame;
        ServiceLocator.GetService<CollisionDetector>().OnEnemyCollision += gameManager.FinishGame;
    }

    private void OnDisable()
    {
        var gameManager = ServiceLocator.GetService<GameManager>();

        ServiceLocator.GetService<CountdownText>().OnGameStarted -= gameManager.StartGame;
        ServiceLocator.GetService<CountdownText>().OnGameResumed -= gameManager.ResumeGame;
        ServiceLocator.GetService<CollisionDetector>().OnEnemyCollision -= gameManager.FinishGame;
    }

    private void OnDestroy()
    {
        ServiceLocator.ClearServices();
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





//// Find variation.
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
