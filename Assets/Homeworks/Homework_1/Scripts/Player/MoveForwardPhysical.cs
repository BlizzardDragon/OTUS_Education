using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MoveForwardPhysical : MonoBehaviour, IGameFixedUpdateListener, IInitListener
{
    public event Action<Vector3> OnMove;


    void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
    {
        var direction = Vector3.forward * fixedDeltaTime; 
        OnMove?.Invoke(direction);
    }
}
