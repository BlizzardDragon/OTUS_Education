using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameworkUnity.Interfaces.Listeners.GameListeners;

public class Test : MonoBehaviour, IGameUpdateListener
{
    public void OnUpdate(float deltaTime)
    {
        Debug.Log(deltaTime);
    }
}
