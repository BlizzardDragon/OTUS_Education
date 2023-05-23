using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class JumpInput : MonoBehaviour, IGameUpdateListener, IInitListener
{
    public Action<int> OnMovedToSide;

    public void OnUpdate(float deltaTime)
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            OnMovedToSide?.Invoke(-1);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            OnMovedToSide?.Invoke(1);
        }
    }
}
