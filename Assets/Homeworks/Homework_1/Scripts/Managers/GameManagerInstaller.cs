using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameManager))]
public sealed class GameManagerInstaller : MonoBehaviour
{
    private void Awake()
    {
        var gameManager = GetComponent<GameManager>();
        IGameListener[] listeners = GetComponentsInChildren<IGameListener>();

        foreach (var listener in listeners)
        {
            gameManager.AddListener(listener);
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
