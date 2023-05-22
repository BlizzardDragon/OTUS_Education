using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MoveForwardPhysical : MonoBehaviour, IGameFixedUpdateListener, IInitListener
{
    public Action<Vector3> OnMove;

    void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
    {
        OnMove?.Invoke(Vector3.forward * fixedDeltaTime);
    }
}
