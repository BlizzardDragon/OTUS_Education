using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Input : MonoBehaviour, IGameFixedUpdateListener
{
    public Action<Vector3> OnMove;


    private void Awake()
    {
        enabled = false;
    }

    void IGameFixedUpdateListener.OnFixedUpdate(float fixedDeltaTime)
    {
        OnMove?.Invoke(Vector3.forward);
    }
}
